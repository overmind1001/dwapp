using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    /// <summary>
    /// Класс для работы с событиями. 
    /// </summary>
    class EventManager
    {
        //Поля
        MetaObjectRepository _repository;           //ссылка на репозиторий

        EventQueue _eventQueue;                                 //очередь
        DatasourceMonitorManager _datasourceMonitorManager;     //монитор источников

        //Конструкторы
        public EventManager(MetaObjectRepository repository)
        {
            _repository = repository;
            _eventQueue = new EventQueue();
            _datasourceMonitorManager = new DatasourceMonitorManager(_repository);
            _datasourceMonitorManager.LoadPlugins();
            _datasourceMonitorManager.DS_Changed +=
                            (ev) =>
                            {
                                this._eventQueue.Add(ev);
                            };
        }

        //Методы
        /// <summary>
        /// Добавляе события в очередь. Отфильтровывает события проверки источников.
        /// </summary>
        /// <param name="events"></param>
        public void AddEvents(List<DataSourceEvent> events)
        {
            List<DataSourceEvent> eventsToRemove = new List<DataSourceEvent>();

            foreach (DataSourceEvent e in events)
            {
                if(e.EventType == "CheckDS_Timer")
                {
                    if( _datasourceMonitorManager.NameRegistered(  e.GetDataSource().DataSourceName ))
                    {
                        
                        _datasourceMonitorManager.CheckDataSource(e.GetDataSource() );//тут запускается проверка источника 
                        eventsToRemove.Add(e);
                    }
                }
                
            }
            foreach (DataSourceEvent e in eventsToRemove)
            {
                events.Remove(e);
            }
            this._eventQueue.AddEvents(events);
        }
        /// <summary>
        /// Получает первое событие из очереди.
        /// </summary>
        /// <returns></returns>
        public DataSourceEvent GetNextEvent()
        {
            if (_eventQueue.Count == 0)
                return null;

            DataSourceEvent e = _eventQueue.PopFirst();//выталкивает первый элемент из очереди
            return e;
        }

        public int EventCount()
        {
            return _eventQueue.Count;
        }
    }
}
