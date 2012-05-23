using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public class DimensionFactory : MetaObjectFactory
    {
        public DimensionFactory(MetaObjectRepository repository)
            :base(repository)
        {
        }
        public override string Name
        {
            get
            {
                return Dimension.Type;
            }
        }
        public override List<AttrNameType> Attributes
        {
            get
            {
                return Dimension.Attributes;
            }
        }

        public override MetaObject CreateObject()
        {
            return new Dimension(_repository);
        }

    }
}
