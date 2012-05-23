using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    class EventGenerator
    {
        //Поля
        MetaObjectRepository _repository;
        //Конструкторы
        public EventGenerator(MetaObjectRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Генерирует события по регламенту
        /// </summary>
        /// <param name="reglamentElements"></param>
        /// <returns></returns>
        public List<DataSourceEvent> GenerateEvents(List<ReglamentElementMetaObject> reglamentElements)
        {
            List<DataSourceEvent> events = new List<DataSourceEvent>();

            DataSourceEvents dses = _repository.LoadMetaObject("DataSourceEvents") as DataSourceEvents;

            foreach (ReglamentElementMetaObject remo in reglamentElements)
            {
                DataSourceEvent dse = _repository.CreateNewMetaObject(MetaObjectType.DataSourceEvent, "") as DataSourceEvent;
                dse.SetDataSource(remo.getDataSource());
                dse.EventType = "TimerEvent";
                _repository.Save(dse);
            }
            return events;
        }
    }
}
