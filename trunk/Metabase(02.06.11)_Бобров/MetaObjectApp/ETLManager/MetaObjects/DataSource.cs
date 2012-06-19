using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using MetaObjectApp;
using ETLManager;

namespace MetaObjectApp
{
    /// <summary>
    /// Источник данных
    /// </summary>
    public class DataSource:MetaObject
    {
        //Метаданные
        public static new string Type = MetaObjectType.DataSource.ToString();
        private static AttrNameType _ant_name = new AttrNameType { Name = "name", Type = AttributeType.String };
        private static AttrNameType _ant_type = new AttrNameType { Name = "type", Type = AttributeType.String };
        private static AttrNameType _ant_url = new AttrNameType { Name = "url", Type = AttributeType.String };
        private static AttrNameType _ant_controlSum = new AttrNameType { Name = "controlSum", Type = AttributeType.String };
        private static AttrNameType _ant_ETL_id = new AttrNameType { Name = "ETL_id", Type = AttributeType.Id };
        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { 
            _ant_name,
            _ant_type,
            _ant_url,
            _ant_controlSum,
            _ant_ETL_id
        };

        //Атрибуты метаобъекта
        private Attribute name;
        private Attribute type;
        private Attribute url;
        private Attribute controlSum;
        private Attribute etl_id;
        //Свойства
        public string DataSourceName
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
        public int ETL_Id
        {
            get
            {
                return (int)etl_id.Value;
            }
            set
            {
                etl_id.Value = value;
            }
        }
        
        //Конструктор
        public DataSource(MetaObjectRepository repository)
            :base(repository)
        {
            TypeName = DataSource.Type;

            name = new Attribute(this, _ant_name);
            type = new Attribute(this, _ant_type);
            url = new Attribute(this, _ant_url);
            controlSum = new Attribute(this, _ant_controlSum);
            etl_id = new Attribute(this, _ant_ETL_id);

            attributes.Add(name);
            attributes.Add(type);
            attributes.Add(url);
            attributes.Add(controlSum);
            attributes.Add(etl_id);
        }

        //Методы
        public ETL GetETL()
        {
            ETL etl = _repository.LoadMetaObject(ETL_Id) as ETL;
            return etl;
        }
        public void SetETL(ETL etl)
        {
            this.ETL_Id = (int)etl.Id;
        }
    }
}
