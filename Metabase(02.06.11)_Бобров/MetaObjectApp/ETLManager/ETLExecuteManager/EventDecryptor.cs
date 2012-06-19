using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    /// <summary>
    /// Класс для расшифровки событий.
    /// </summary>
    class EventDecryptor
    {
        //Методы
        /// <summary>
        /// Метод для расшифровки сообщения.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Dictionary<string,string> DecryptEvent(DataSourceEvent e)
        {
            //получить событие, узнать про датасорс и етл
            DataSource ds = e.GetDataSource();
            ETL etl = ds.GetETL();

            Dictionary<string, string> result = new Dictionary<string, string>();
            result["path"]=etl.AssemblyPath;
            result["args"]=etl.AssemblyArgs;
            return result;
        }
    }
}
