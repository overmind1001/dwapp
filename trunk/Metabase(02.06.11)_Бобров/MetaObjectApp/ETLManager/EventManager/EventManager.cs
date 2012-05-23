using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETLManager
{
    class EventManager
    {
        EventQueue _eventQueue;
        DatasourceMonitorManager _datasourceMonitorManager;

        internal void AddEvents(List<DataSourceEvent> events)
        {
            throw new NotImplementedException();
        }

        internal DataSourceEvent GetNextEvent()
        {
            throw new NotImplementedException();
        }
    }
}
