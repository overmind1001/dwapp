using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    class ReglamentReader
    {
        //Поля
        MetaObjectRepository _repository;



        //Конструкторы
        public ReglamentReader(MetaObjectRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Получает список элементов регламента, которые к текщему моменту "сработали"
        /// </summary>
        /// <returns></returns>
        public List<ReglamentElementMetaObject> GetCurrentTasks()
        {
            List<ReglamentElementMetaObject> currentTasks = new List<ReglamentElementMetaObject>();

            ReglamentMetaObject rmo = _repository.LoadMetaObject("Reglament") as ReglamentMetaObject;
            if (rmo == null)
                throw new Exception("Нет метаобъекта Reglament");

            //Получаем элементы регламента. Смотрим какие из них по дате активации совпадают с текущим временем. Добавляем их в список
            //TODO
            return currentTasks;
        }
    }
}
