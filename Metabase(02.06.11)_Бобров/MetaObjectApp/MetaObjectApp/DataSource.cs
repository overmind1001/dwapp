﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace MetaObjectApp
{
    public class DataSource:MetaObject
    {
        //Метаданные
        public static new string Type = MetaObjectType.DataSource.ToString();
        private static AttrNameType _ant_type = new AttrNameType { Name = "type", Type = AttributeType.String };
        private static AttrNameType _ant_url = new AttrNameType { Name = "url", Type = AttributeType.String };
        private static AttrNameType _ant_controlSum = new AttrNameType { Name = "controlSum", Type = AttributeType.String };
        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { 
            _ant_type,
            _ant_url,
            _ant_controlSum
        };

        //Атрибуты метаобъекта
        private Attribute type;
        private Attribute url;
        private Attribute controlSum;
        //Свойства
        public string DataSourceType
        {
            get
            {
                return type.Value.ToString();
            }
            set
            {
                type.Value = value;
            }
        }
        public string Url
        {
            get
            {
                return url.Value.ToString();
            }
            set
            {
                url.Value = value;
            }
        }
        public string ControlSum
        {
            get
            {
                return controlSum.Value.ToString();
            }
            set
            {
                controlSum.Value = value;
            }
        }

        public DataSource(MetaObjectRepository repository)
            :base(repository)
        {
            TypeName = DataSource.Type;

            type = new Attribute(this, _ant_type);
            url = new Attribute(this, _ant_url);
            controlSum = new Attribute(this, _ant_controlSum);

            attributes.Add(type);
            attributes.Add(url);
            attributes.Add(controlSum);
        }
    }
}
