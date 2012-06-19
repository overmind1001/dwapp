using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ETLManager
{
    /// <summary>
    /// Класс для запуска ETL на выполнение
    /// </summary>
    class ETLExecuteManager
    {
        //Поля
        EventDecryptor _eventDecryptor;     //ссылка на расшифровщик событий

        //Конструкторы
        public ETLExecuteManager()
        {
            _eventDecryptor = new EventDecryptor();
        }
        //Методы
        /// <summary>
        /// Обработка события.
        /// </summary>
        /// <param name="e"></param>
        public void ProcessEvent(DataSourceEvent e)
        {
            Dictionary<string, string> pathToETLAndArgs = _eventDecryptor.DecryptEvent(e);
            ProcessStartInfo info = new ProcessStartInfo(pathToETLAndArgs["path"], pathToETLAndArgs["args"]);
            info.UseShellExecute = true;
            Process p = Process.Start(info);
            Console.WriteLine("Запускаем сборку ETL");
        }
    }
}
