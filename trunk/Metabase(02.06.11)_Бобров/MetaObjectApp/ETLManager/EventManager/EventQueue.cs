using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETLManager
{
    class EventQueue : List<DataSourceEvent>
    {
        public void AddEvents(List<DataSourceEvent> events)
        {
            foreach (DataSourceEvent e in events)
            {
                this.Add(e);
            }
        }
        /// <summary>
        /// Выталкивает первый элемент
        /// </summary>
        /// <returns></returns>
        public DataSourceEvent PopFirst()
        {
            if (this.Count > 0)
            {
                DataSourceEvent e = this[0];
                this.RemoveAt(0);
                return e;
            }
            else
                return null;
            
        }
    }
}
