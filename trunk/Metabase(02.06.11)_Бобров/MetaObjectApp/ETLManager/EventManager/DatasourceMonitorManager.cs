using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public delegate void DS_ChangedHandler(DataSourceEvent e);
    public delegate void AcyncDelegate();

    class DatasourceMonitorManager
    {
        //Поля
        MetaObjectRepository _repository;

        private string PluginsDirPath;                  //Директория с плагинами
        private List<IDatasourceMonitor> monitorList;   //Список плагинов

        //События
        public event DS_ChangedHandler DS_Changed;

        public DatasourceMonitorManager(MetaObjectRepository repository)
        {
            this._repository = repository;
            PluginsDirPath = "//Plugins";
        }

        private void Raise_DS_Changed(DataSourceEvent e)
        {
            if (DS_Changed != null)
                DS_Changed(e);
        }

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
        //TODO
        public void CheckDataSource(MetaObjectApp.DataSource dataSource)
        {
            //Делаем асинхронный делегат, который запускает проверку источника

            IDatasourceMonitor dsm = monitorList.Find((e) => { return e.DataSourceName == dataSource.DataSourceName; });
            if (dsm==null)
                return;

            AcyncDelegate d = new AcyncDelegate(() => {
                if (dsm.DataSourceChanged())
                {
                  s  Raise_DS_Changed(new DataSourceEvent(_repository) {EventType="sdfsf Надо сохранить евент в репозитории?" });
                }
            });

            d.BeginInvoke(null, null);
        }


    }
}
