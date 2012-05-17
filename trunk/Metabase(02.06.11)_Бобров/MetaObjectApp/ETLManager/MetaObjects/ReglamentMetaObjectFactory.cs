using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public class ReglamentMetaObjectFactory : MetaObjectFactory
    {
        public override string Name
        {
            get
            {
                return ReglamentMetaObject.Type;
            }
        }
        public override List<AttrNameType> Attributes
        {
            get
            {
                return ReglamentMetaObject.Attributes;
            }
        }

        public override MetaObject CreateObject()
        {
            return new ReglamentMetaObject();
        }
    }
}
