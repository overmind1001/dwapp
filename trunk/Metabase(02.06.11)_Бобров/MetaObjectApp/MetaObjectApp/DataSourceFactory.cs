using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace MetaObjectApp
{
    class DataSourceFactory:MetaObjectFactory
    {
        
        public DataSourceFactory()
        {
        }

        public override string Name
        {
            get
            {
                return DataSource.Type;
            }
        }
        public override List<AttrNameType> Attributes
        {
            get
            {
                return DataSource.Attributes;
            }
        }

        public override MetaObject CreateObject()
        {
            return new DataSource();
        }
    }
}
