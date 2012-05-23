using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public class CubeFactory : MetaObjectFactory
    {
        public CubeFactory(MetaObjectRepository repository)
            :base(repository)
        {
        }
        public override string Name
        {
            get
            {
                return Cube.Type;
            }
        }
        public override List<AttrNameType> Attributes
        {
            get
            {
                return Cube.Attributes;
            }
        }

        public override MetaObject CreateObject()
        {
            return new Cube(_repository);
        }
    }
}
