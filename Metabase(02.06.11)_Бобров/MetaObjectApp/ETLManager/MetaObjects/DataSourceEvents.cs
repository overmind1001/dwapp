using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public class DataSourceEvents : MetaObject
    {
        //Поля
        public static new string Type = MetaObjectType.DataSourceEvents.ToString();
       
        private static AttrNameType _ant_attrList = new AttrNameType { Name = "elementList", Type = AttributeType.List };
        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { _ant_attrList};

        //Атрибуты
        private MetaObjectApp.Attribute elementList;

        public List<int> DataSourceEventsIds
        {
            get
            {
                return elementList.Value as List<int>;
            }
            set
            {
                elementList.Value = value;
            }
        }
        //Конструктор
        public DataSourceEvents(MetaObjectRepository repository)
            : base(repository)
        {
            this.TypeName = DataSourceEvents.Type;
            elementList = new MetaObjectApp.Attribute(this, _ant_attrList);
            attributes.Add(elementList);
        }
        //Методы
        public List<DataSourceEvent> GetDataSourceEvents()
        {
            List<int> list = elementList.Value as List<int>;
            List<DataSourceEvent> dataSourceEvents = new List<DataSourceEvent>();
            foreach (int inx in list)
            {
                DataSourceEvent remo = _repository.LoadMetaObject(inx) as DataSourceEvent;
                dataSourceEvents.Add(remo);
            }
            return dataSourceEvents;
        }
        public void SetDataSourceEvents(List<DataSourceEvent> dataSourceEvents)
        {
            List<int> list = new List<int>();

            foreach (DataSourceEvent dse in dataSourceEvents)
            {
                list.Add((int)dse.Id);
            }
            elementList.Value = list;
        }
        public void AddDataSourceEvent(DataSourceEvent dse)
        {
            if (elementList.Value == null)
                elementList.Value = new List<int>();
            ((List<int>)elementList.Value).Add((int)dse.Id);
            elementList.Changed = true;
        }
        public void RemoveDataSourceEvent(DataSourceEvent dse)
        {
            ((List<int>)elementList.Value).Remove((int)dse.Id);
            elementList.Changed = true;
        }
    }
}
