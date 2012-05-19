using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public class TestMetaObject : MetaObject
    {
        public static new string Type = MetaObjectType.TestMetaObject.ToString();

        private static AttrNameType _ant_attrString = new AttrNameType { Name = "attrString", Type = AttributeType.String };
        private static AttrNameType _ant_attrBigint = new AttrNameType { Name = "attrBigint", Type = AttributeType.Bigint };
        private static AttrNameType _ant_attrDouble = new AttrNameType { Name = "attrDouble", Type = AttributeType.Double };
        private static AttrNameType _ant_attrList = new AttrNameType { Name = "attrList", Type = AttributeType.List };
        private static AttrNameType _ant_attrId = new AttrNameType { Name = "attrId", Type = AttributeType.Id };
        private static AttrNameType _ant_attrBinary = new AttrNameType { Name = "attrBinary", Type = AttributeType.Binary };

        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { 
            _ant_attrString,
            _ant_attrBigint,
            _ant_attrDouble,
            _ant_attrList,
            _ant_attrId,
            _ant_attrBinary
        };

        
        private MetaObjectApp.Attribute _attrString;
        private MetaObjectApp.Attribute _attrBigint;
        private MetaObjectApp.Attribute _attrDouble;
        private MetaObjectApp.Attribute _attrList;
        private MetaObjectApp.Attribute _attrId;
        private MetaObjectApp.Attribute _attrBinary;
        //Атрибуты
        public MetaObjectApp.Attribute attrString { get { return _attrString; } }
        public MetaObjectApp.Attribute attrBigint { get { return _attrBigint; } }
        public MetaObjectApp.Attribute attrDouble { get { return _attrDouble; } }
        public MetaObjectApp.Attribute attrList { get { return _attrList; } }
        public MetaObjectApp.Attribute attrId { get { return _attrId; } }
        public MetaObjectApp.Attribute attrBinary { get { return _attrBinary; } }

        public TestMetaObject(MetaObjectRepository repository)
            :base(repository)
        {
            this.TypeName = TestMetaObject.Type;

            _attrString = new MetaObjectApp.Attribute(this, _ant_attrString);
            _attrBigint = new MetaObjectApp.Attribute(this, _ant_attrBigint);
            _attrDouble = new MetaObjectApp.Attribute(this, _ant_attrDouble);
            _attrList = new MetaObjectApp.Attribute(this, _ant_attrList);
            _attrId = new MetaObjectApp.Attribute(this, _ant_attrId);
            _attrBinary = new MetaObjectApp.Attribute(this, _ant_attrBinary);

            attributes.Add(attrString);
            attributes.Add(attrBigint);
            attributes.Add(attrDouble);
            attributes.Add(attrList);
            attributes.Add(attrId);
            attributes.Add(attrBinary);
        }
    }
}
