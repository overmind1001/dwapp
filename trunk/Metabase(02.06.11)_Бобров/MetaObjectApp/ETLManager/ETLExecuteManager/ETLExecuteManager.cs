using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETLManager
{
    class ETLExecuteManager
    {
        EventDecryptor _eventDecryptor;
        /// <summary>
        /// Запускает ETL сборку
        /// </summary>
        void ExecuteETL()
        {
        }

        internal void ProcessEvent(DataSourceEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
