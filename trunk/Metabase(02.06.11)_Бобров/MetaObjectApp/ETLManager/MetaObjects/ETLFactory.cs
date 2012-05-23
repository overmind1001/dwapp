using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public class ETLFactory : MetaObjectFactory
    {
        public ETLFactory(MetaObjectRepository repository)
            :base(repository)
        {}
        public override string Name
        {
            get
            {
                return ETL.Type;
            }
        }
        public override List<AttrNameType> Attributes
        {
            get
            {
                return ETL.Attributes;
            }
        }

        public override MetaObject CreateObject()
        {
            return new ETL(_repository);
        }
    }
}
