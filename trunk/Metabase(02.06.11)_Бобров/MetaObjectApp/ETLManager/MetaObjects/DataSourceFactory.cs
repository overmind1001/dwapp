using System;
using System.Collections.Generic;
using System.Text;
using MetaObjectApp;

namespace MetaObjectApp
{
    public class DataSourceFactory:MetaObjectFactory
    {
        public DataSourceFactory(MetaObjectRepository repository)
            :base(repository)
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
            return new DataSource(_repository);
        }
    }
}
