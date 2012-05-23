using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public class DataSourceEvent : MetaObject
    {
        public static new string Type = MetaObjectType.DataSourceEvent.ToString();
        private static AttrNameType _ant_attrEventType = new AttrNameType { Name = "eventType", Type = AttributeType.String };
        private static AttrNameType _ant_attrDataSourceId = new AttrNameType { Name = "dataSourceId", Type = AttributeType.Id };

        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { 
            _ant_attrEventType,
            _ant_attrDataSourceId
        };

        //Атрибуты
        private MetaObjectApp.Attribute eventType;
        private MetaObjectApp.Attribute dataSourceId;
        //Свойства
        public string EventType
        {
            get
            {
                return eventType.Value.ToString();
            }
            set
            {
                eventType.Value = value;
            }
        }
        public int DataSourceId
        {
            get
            {
                return (int)dataSourceId.Value;
            }
            set
            {
                dataSourceId.Value = value;
            }
        }

        public DataSourceEvent(MetaObjectRepository repository)
            : base(repository)
        {
            this.TypeName = DataSourceEvent.Type;
            eventType = new MetaObjectApp.Attribute(this, _ant_attrEventType);
            dataSourceId = new MetaObjectApp.Attribute(this, _ant_attrDataSourceId);

            attributes.Add(eventType);
            attributes.Add(dataSourceId);
        }

        public DataSource GetDataSource()
        {
            DataSource ds = _repository.LoadMetaObject(DataSourceId) as DataSource;
            return ds;
        }
        public void SetDataSource(DataSource dataSource)
        {
            DataSourceId =(int) dataSource.Id;
        }
     
    }
}
