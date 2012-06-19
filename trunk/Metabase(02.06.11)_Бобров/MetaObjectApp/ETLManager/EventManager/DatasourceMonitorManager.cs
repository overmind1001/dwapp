using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;
using System.IO;
using System.Reflection;
using IDatasourceMonitorNS;

namespace ETLManager
{
    /// <summary>
    /// Делегат для оповещения об изменении источника.
    /// </summary>
    /// <param name="e">Событие</param>
    public delegate void DS_ChangedHandler(DataSourceEvent e);
    /// <summary>
    /// Делегат для запуска проверки источника в отдельном потоке.
    /// </summary>
    public delegate void AcyncDelegate();

    /// <summary>
    /// Класс, который организует работу с плагинами, следящими за источниками
    /// </summary>
    class DatasourceMonitorManager
    {
        //Поля
        MetaObjectRepository _repository;               //ссылка на репозиторий

        private string PluginsDirPath;                  //Директория с плагинами
        private List<IDatasourceMonitor> monitorList;   //Список плагинов

        //События
        public event DS_ChangedHandler DS_Changed;      //событие об изменении в источнике

        //Конструкторы
        public DatasourceMonitorManager(MetaObjectRepository repository)
        {
            this._repository = repository;
            PluginsDirPath = "Plugins";
            monitorList = new List<IDatasourceMonitor>();
        }

        //Методы
        private void Raise_DS_Changed(DataSourceEvent e)
        {
            if (DS_Changed != null)
                DS_Changed(e);
        }
        /// <summary>
        /// Проверяет, зарегистрирован ли нужный плагин.
        /// </summary>
        /// <param name="name">Имя плагина</param>
        /// <returns></returns>
        public bool NameRegistered(string name)
        {
            foreach(IDatasourceMonitor dsm in monitorList)
            {
                if (dsm.DataSourceName == name)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Запускает проверку источника данных в отдельном потоке.
        /// </summary>
        /// <param name="dataSource">Проверяемый источник данных</param>
        public void CheckDataSource(MetaObjectApp.DataSource dataSource)
        {
            //Делаем асинхронный делегат, который запускает проверку источника
            IDatasourceMonitor dsm = monitorList.Find((e) => { return e.DataSourceName == dataSource.DataSourceName; });
            if (dsm==null)
                return;

            AcyncDelegate d = new AcyncDelegate(() => {
                string newControlSum = dsm.controlSum();
                if (newControlSum  != dataSource.ControlSum)//контрольная сумма изменилась
                {
                    //сохраняем новую контрольную сумму
                    dataSource.ControlSum = newControlSum;
                    _repository.Save(dataSource);

                    //создаем новый метаобъект "событие"
                    DataSourceEvent newEvent = (DataSourceEvent) _repository.CreateNewMetaObject(MetaObjectType.DataSourceEvent,"");
                    newEvent.EventType = "DSchanged";
                    newEvent.SetDataSource(dataSource);
                    _repository.Save(newEvent);

                    //добавляем этот метаобъект в метаобъект "события"
                    DataSourceEvents events = _repository.LoadMetaObject("DataSourceEvents") as DataSourceEvents;
                    events.AddDataSourceEvent(newEvent);
                    _repository.Save(events);

                    Raise_DS_Changed(newEvent);
                }
            });

            d.BeginInvoke(null, null);
        }
        /// <summary>
        /// Загружает плагины из папки плагинов.
        /// </summary>
        public void LoadPlugins()
        {
            monitorList.Clear();
            // папка с плагинами
            string folder = System.AppDomain.CurrentDomain.BaseDirectory+PluginsDirPath;
            // dll-файлы в этой папке
            string[] files = Directory.GetFiles(folder, "*.dll");
            foreach (string file in files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);
                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("IDatasourceMonitor");
                        if (iface != null)
                        {
                            IDatasourceMonitor plugin = (IDatasourceMonitor)Activator.CreateInstance(type);
                            monitorList.Add(plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка загрузки плагина\n" + ex.Message);
                }
            }
        }
    }
}
