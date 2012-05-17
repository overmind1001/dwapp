using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;
using System.Data.SqlClient;

namespace ETLManager
{
    class ReglamentMetaObject : MetaObject
    {
        public const string ELEMENT_LIST = "elementList";


        public static new string Type = MetaObjectType.Reglament.ToString();
        private static AttrNameType _ant_attrList = new AttrNameType { Name = "elementList", Type = AttributeType.List };
        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { _ant_attrList};

        //Атрибуты
        public MetaObjectApp.Attribute elementList;

        public ReglamentMetaObject()
        {
            this.TypeName = ReglamentMetaObject.Type;
            elementList = new MetaObjectApp.Attribute(this, ELEMENT_LIST, AttributeType.List, "");
            attributes.Add(elementList);
        }

        internal List<ReglamentElementMetaObject> GetReglamentElements()
        {
            throw new NotImplementedException();
        }
    }
}
