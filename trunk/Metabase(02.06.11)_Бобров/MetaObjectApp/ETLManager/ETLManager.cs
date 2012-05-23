using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlClient;
using MetaObjectApp;

namespace ETLManager
{
    public class ETLManager
    {
        //Поля
        ReglamentManager    _reglamentManager;
        EventManager        _eventManager;
        ETLExecuteManager   _etlExecuteManager;

        bool    _isRunning;          //признак того, что менеджер уже работает
        MetaObjectRepository _repository;   //ссылка на репозиторий
        Thread  _thread;             //поток управления. В этом потоке выполняется чтение регламента, проброска событий, запуск на выполнение ETL-сборок.

        //Свойства
        bool    IsRunning
        {
            get
            {
                return _isRunning;
            }
        }
        MetaObjectRepository Repository
        {
            get
            {
                return _repository;
            }
            set
            {
                _repository = value;
            }
        }

        //конструкторы
        public  ETLManager()
        {
            Init();
        }
        public  ETLManager(MetaObjectRepository repository)
        {
            Init();
            _repository = repository;
        }

        //методы

        /// <summary>
        /// Начинает ципл чтения регламента, запуска сборок, слежения за источниками данных.
        /// </summary>
        /// <returns>Возврацает результат запуска.</returns>
        public bool Start()
        {
            if (IsRunning)
                return false;
            if (_repository == null)
                return false;
            if (!CheckConnection())
                return false;

            _thread = new Thread(ThreadLoop);
            _thread.Start();

            _isRunning = true;
            return true;
        }
        /// <summary>
        /// Заканчивает работу менеджера ETL
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
            if (_thread != null)
                _thread.Abort();
        }

        /// <summary>
        /// Метод, выполняющийся в отдельном потоке. Это главный управляющий поток работы менеджера ETL.
        /// </summary>
        void ThreadLoop()
        {
            while (_isRunning)
            {
                List<DataSourceEvent> events = _reglamentManager.ReadReglament();
                if (events.Count > 0)
                {//добавляем в очередь событий
                    _eventManager.AddEvents(events);
                }

                //Получение из очереди очередного события
                DataSourceEvent e = _eventManager.GetNextEvent();
                if (e!=null)
                {
                    _etlExecuteManager.ProcessEvent(e);
                }
            }
        }

        bool CheckConnection()
        {
            return true;
        }

        void Init()
        {
            _isRunning = false;
            _reglamentManager = new ReglamentManager(_repository);
            _eventManager = new EventManager();
            _etlExecuteManager = new ETLExecuteManager();
        }
    }
}
