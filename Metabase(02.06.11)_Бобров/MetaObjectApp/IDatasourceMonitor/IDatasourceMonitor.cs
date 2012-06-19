using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDatasourceMonitorNS
{
    /// <summary>
    /// Интерфейс для подключения плагинов, которые проверяют состояние источника данных.
    /// </summary>
    public interface IDatasourceMonitor
    {
        string Name { get; }            //Имя плагина
        string DataSourceName { get; }  //Название источника данных
        string controlSum();            //Вычисление контрольной суммы
    }
}
