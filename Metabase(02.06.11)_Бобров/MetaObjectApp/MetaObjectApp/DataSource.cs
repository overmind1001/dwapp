using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MetaObjectApp
{
    class DataSource:MetaObject
    {
        //Метаданные
        public static new string Type = MetaObjectType.DataSource.ToString();
        private static AttrNameType _ant_source = new AttrNameType { Name = "source", Type = AttributeType.String };
        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { _ant_source };

        //Атрибуты метаобъекта
        public Attribute source;

        public DataSource()
        {
            TypeName = DataSource.Type;
            source=new Attribute(this, _ant_source);
            attributes.Add(source);
        }
    }
}
