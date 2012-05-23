using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public class CubesFactory : MetaObjectFactory
    {
        public CubesFactory(MetaObjectRepository repository)
            :base(repository)
        {}
        public override string Name
        {
            get
            {
                return Cubes.Type;
            }
        }
        public override List<AttrNameType> Attributes
        {
            get
            {
                return Cubes.Attributes;
            }
        }

        public override MetaObject CreateObject()
        {
            return new Cubes(_repository);
        }
    }
}
