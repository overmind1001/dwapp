using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETLManager
{
    /// <summary>
    /// Интерфейс для подключения плагинов, которые проверяют состояние источника данных.
    /// </summary>
    interface IDatasourceMonitor
    {
        string Name { get; } //Имя плагина
        string DataSourceName { get; }

        bool DataSourceChanged();

    }
}
