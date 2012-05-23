using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public class ETLsFactory : MetaObjectFactory
    {
        public ETLsFactory(MetaObjectRepository repository)
            :base(repository)
        {}
        public override string Name
        {
            get
            {
                return ETLs.Type;
            }
        }
        public override List<AttrNameType> Attributes
        {
            get
            {
                return ETLs.Attributes;
            }
        }

        public override MetaObject CreateObject()
        {
            return new ETLs(_repository);
        }
    }
}
