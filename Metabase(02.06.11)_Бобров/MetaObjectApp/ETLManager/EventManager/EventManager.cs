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

        internal void AddEvents(List<ETLEvent> events)
        {
            throw new NotImplementedException();
        }

        internal ETLEvent GetNextEvent()
        {
            throw new NotImplementedException();
        }
    }
}
