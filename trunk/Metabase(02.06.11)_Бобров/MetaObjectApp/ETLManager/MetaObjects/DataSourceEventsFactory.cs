using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public class DataSourceEventsFactory : MetaObjectFactory
    {
        public DataSourceEventsFactory(MetaObjectRepository repository)
            :base(repository)
        {
        }
        public override string Name
        {
            get
            {
                return DataSourceEvents.Type;
            }
        }
        public override List<AttrNameType> Attributes
        {
            get
            {
                return DataSourceEvents.Attributes;
            }
        }

        public override MetaObject CreateObject()
        {
            return new DataSourceEvents(_repository);
        }
    }
}
