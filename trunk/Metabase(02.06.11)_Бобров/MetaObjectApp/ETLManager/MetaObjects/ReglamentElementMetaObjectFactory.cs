﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    class ReglamentElementMetaObjectFactory : MetaObjectApp.MetaObjectFactory
    {
        public override string Name
        {
            get
            {
                return ReglamentElementMetaObject.Type;
            }
        }
        public override List<AttrNameType> Attributes
        {
            get
            {
                return ReglamentElementMetaObject.Attributes;
            }
        }

        public override MetaObject CreateObject()
        {
            return new ReglamentElementMetaObject();
        }
    }
}
