using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    /// <summary>
    /// Класс, отвечающиц за работу с регламентом
    /// </summary>
    class ReglamentManager
    {
        //Поля
        ReglamentReader _reglamentReader;
        EventGenerator _eventGenerator;

        MetaObjectRepository _repository;
        //Свойства

        //Конструкторы
        public ReglamentManager(MetaObjectRepository repository)
        {
            _repository = repository;
            Init();
        }
        //Методы

        /// <summary>
        /// Читает регламент
        /// </summary>
        /// <returns>Возвращает список произошедших событий.</returns>
        public List<DataSourceEvent> ReadReglament()
        {
            List<ReglamentElementMetaObject> reglamentElements = _reglamentReader.GetCurrentTasks();    //получаем элементы регламента, которые сработали к текущему моменту
            List<DataSourceEvent> eventList = _eventGenerator.GenerateEvents(reglamentElements);               //создаем события для полученных элементов регламента
            return eventList;
        }

        void Init()
        {
            _reglamentReader = new ReglamentReader(_repository);
            _eventGenerator = new EventGenerator(_repository);

        }
    }
}
