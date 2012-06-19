using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    /// <summary>
    /// Класс для генерации события по элементам регламента
    /// </summary>
    class EventGenerator
    {
        //Поля
        MetaObjectRepository _repository;
        //Конструкторы
        public EventGenerator(MetaObjectRepository repository)
        {
            _repository = repository;
        }
        //методы
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
                if (remo.ReglamentElementType == "ExecETL")
                {
                    dse.EventType = "ExecETL_Timer";
                }
                else if (remo.ReglamentElementType == "CheckDS")
                {
                    dse.EventType = "CheckDS_Timer";
                }
                else 
                {
                    throw new Exception("Неизвестный тип элемента регламента");
                }
                
                _repository.Save(dse);
                dses.AddDataSourceEvent(dse);
                events.Add(dse);

                //генерация новых элементов регламента в связи с периодичностью 
                if (remo.Period.TotalSeconds > 0.1)
                {
                    remo.LastRunTime = remo.NextRunTime;
                    remo.NextRunTime += remo.Period;
                    _repository.Save(remo);
                }
            }
            _repository.Save(dses);
            return events;
        }
    }
}
