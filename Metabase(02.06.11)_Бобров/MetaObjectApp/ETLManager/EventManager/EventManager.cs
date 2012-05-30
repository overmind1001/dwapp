using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    class EventManager
    {
        MetaObjectRepository _repository;

        EventQueue _eventQueue;
        DatasourceMonitorManager _datasourceMonitorManager;

        //Конструкторы
        public EventManager(MetaObjectRepository repository)
        {
            _repository = repository;
            _eventQueue = new EventQueue();
            _datasourceMonitorManager = new DatasourceMonitorManager(_repository);
        }

        //Методы
        public void AddEvents(List<DataSourceEvent> events)
        {
            List<DataSourceEvent> eventsToRemove = new List<DataSourceEvent>();

            foreach (DataSourceEvent e in events)
            {
                if(e.EventType == "CheckDS_Timer")
                {
                    if( _datasourceMonitorManager.NameRegistered(  e.GetDataSource().DataSourceName ))
                    {
                        _datasourceMonitorManager.DS_Changed += 
                            (ev) => 
                        {//осторожно, может сделать критическую секцию?
                            this._eventQueue.Add(ev);
                        };
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

        public DataSourceEvent GetNextEvent()
        {
            if (_eventQueue.Count == 0)
                return null;

            DataSourceEvent e = _eventQueue.PopFirst();//выталкивает первый элемент из очереди
            return e;
        }
    }
}
