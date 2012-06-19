using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    /// <summary>
    /// Метаобъект элемент регламента
    /// </summary>
    public class ReglamentElementMetaObject : MetaObject
    {
        //поля
        public static new string Type = MetaObjectType.ReglamentElement.ToString();
        private static AttrNameType _ant_attrEnabled = new AttrNameType { Name = "enabled", Type = AttributeType.Bigint };
        private static AttrNameType _ant_attrDataSourceLink = new AttrNameType { Name = "dataSourceId", Type = AttributeType.Id };
        private static AttrNameType _ant_attrLastRunTime = new AttrNameType { Name = "lastRunTime", Type = AttributeType.String };
        private static AttrNameType _ant_attrPeriod = new AttrNameType { Name = "period", Type = AttributeType.String };
        private static AttrNameType _ant_attrNextRunTime = new AttrNameType { Name = "nextRunTime", Type = AttributeType.String };
        private static AttrNameType _ant_type = new AttrNameType { Name = "type", Type = AttributeType.String };
        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { 
            _ant_attrEnabled,
            _ant_attrDataSourceLink,
            _ant_attrLastRunTime,
            _ant_attrPeriod,
            _ant_attrNextRunTime,
            _ant_type
        };

        //Атрибуты
        private MetaObjectApp.Attribute enabled;
        private MetaObjectApp.Attribute dataSourceId;
        private MetaObjectApp.Attribute lastRunTime;
        private MetaObjectApp.Attribute period;
        private MetaObjectApp.Attribute nextRunTime;
        private MetaObjectApp.Attribute type;

        //свойства
        public bool Enabled
        {
            get
            {
                return Convert.ToInt64( enabled.Value) != 0;
            }
            set
            {
                if (value)
                {
                    enabled.Value = 1;
                }
                else
                {
                    enabled.Value = 0;
                }
            }
        }
        public Int64 DataSourceId
        {
            get
            {
                return (Int64)dataSourceId.Value;
            }
            set
            {
                dataSourceId.Value = value;
            }
        }
        public DateTime LastRunTime
        {
            get
            {
                return DateTime.Parse(lastRunTime.ToString());
            }
            set
            {
                lastRunTime.Value = value.ToString();
            }
        }
        public TimeSpan Period
        {
            get
            {
                return TimeSpan.Parse(period.ToString());
            }
            set
            {
                period.Value = value.ToString();
            }
        }
        public DateTime NextRunTime
        {
            get
            {
                return DateTime.Parse(nextRunTime.ToString());
            }
            set
            {
                nextRunTime.Value = value.ToString();
            }
        }
        public string ReglamentElementType
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

        //Конструкторы
        public ReglamentElementMetaObject(MetaObjectRepository repository)
            : base(repository)
        {
            this.TypeName = ReglamentElementMetaObject.Type;

            enabled = new MetaObjectApp.Attribute(this, _ant_attrEnabled);
            dataSourceId = new MetaObjectApp.Attribute(this, _ant_attrDataSourceLink);
            lastRunTime = new MetaObjectApp.Attribute(this, _ant_attrLastRunTime);
            period = new MetaObjectApp.Attribute(this, _ant_attrPeriod);
            nextRunTime = new MetaObjectApp.Attribute(this, _ant_attrNextRunTime);
            type = new MetaObjectApp.Attribute(this, _ant_type);

            attributes.Add(enabled);
            attributes.Add(dataSourceId);
            attributes.Add(lastRunTime);
            attributes.Add(period);
            attributes.Add(nextRunTime);
            attributes.Add(type);
        }
        //методы
        public DataSource getDataSource()
        {
            DataSource ds = _repository.LoadMetaObject((int)DataSourceId) as DataSource;
            return ds;
        }
        public void SetDataSource(DataSource ds)
        {
            DataSourceId = ds.Id;
        }
    }
}
