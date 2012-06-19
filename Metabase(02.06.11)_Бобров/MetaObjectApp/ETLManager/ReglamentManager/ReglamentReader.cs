using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    /// <summary>
    /// Класс для чтения регламента
    /// </summary>
    public class ReglamentReader
    {
        //Поля
        MetaObjectRepository _repository;

        //Конструкторы
        public ReglamentReader(MetaObjectRepository repository)
        {
            _repository = repository;
        }

        //методы
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
            List<ReglamentElementMetaObject> regElMO = rmo.GetReglamentElements();
            int ol = Console.CursorLeft;
            int ot = Console.CursorTop;
            foreach (ReglamentElementMetaObject re in regElMO)
            {
                if (!re.Enabled)
                    continue;

                int sec = (int)(re.NextRunTime - DateTime.Now).TotalSeconds;

                if(sec>0)
                    Console.Write("\n{0}:{1}|",re.ReglamentElementType, sec);
                

                if (sec < 2 && sec > -1)
                {
                    currentTasks.Add(re);
                }
            }
            Console.SetCursorPosition(ol, ot);

            return currentTasks;
        }
    }
}
