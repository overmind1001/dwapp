using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public class Dimension : MetaObject
    {
        public static new string Type = MetaObjectType.Dimension.ToString();

        private static AttrNameType _ant_attrName = new AttrNameType { Name = "name", Type = AttributeType.String };
        private static AttrNameType _ant_attrTable = new AttrNameType { Name = "table", Type = AttributeType.String };

        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { 
            _ant_attrName,
            _ant_attrTable
        };

        //Атрибуты
        private MetaObjectApp.Attribute name;
        private MetaObjectApp.Attribute table;
        //Свойства
        public string Name
        {
            get
            {
                return name.Value.ToString();
            }
            set
            {
                name.Value = value;
            }
        }
        public string Table
        {
            get
            {
                return table.Value.ToString();
            }
            set
            {
                table.Value = value;
            }
        }

        public Dimension(MetaObjectRepository repository)
            : base(repository)
        {
            this.TypeName = Dimension.Type;

            name = new MetaObjectApp.Attribute(this, _ant_attrName);
            table = new MetaObjectApp.Attribute(this, _ant_attrTable);

            attributes.Add(name);
            attributes.Add(table);
        }
    }
}
