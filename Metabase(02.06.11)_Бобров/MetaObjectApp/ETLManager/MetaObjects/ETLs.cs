using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public class ETLs : MetaObject
    {
        public static new string Type = MetaObjectType.ETLs.ToString();
        
        private static AttrNameType _ant_attrList = new AttrNameType { Name = "ETLList", Type = AttributeType.List };
        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { _ant_attrList};

        //Атрибуты
        private MetaObjectApp.Attribute elementList;
        //Свойства
        public List<int> ETLIds
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
        public ETLs(MetaObjectRepository repository)
            : base(repository)
        {
            this.TypeName = ETLs.Type;
            elementList = new MetaObjectApp.Attribute(this, _ant_attrList);
            attributes.Add(elementList);
        }

        public List<ETL> GetETLs()
        {
            List<int> list = elementList.Value as List<int>;
            List<ETL> etls = new List<ETL>();
            foreach (int inx in list)
            {
                ETL etl = _repository.LoadMetaObject(inx) as ETL;
                etls.Add(etl);
            }
            return etls;
        }
        public void SetETLs(List<ETL> ETLs)
        {
            List<int> list = new List<int>();

            foreach (ETL etl in ETLs)
            {
                list.Add((int)etl.Id);
            }
            elementList.Value = list;
        }
        public void AddETL(ETL etl)
        {
            if (elementList.Value == null)
            {
                elementList.Value = new List<int>();
            }
            ((List<int>)elementList.Value).Add((int)etl.Id);
            elementList.Changed = true;
        }
        public void RemoveReglamentElement(ETL etl)
        {
            ((List<int>)elementList.Value).Remove((int)etl.Id);
            elementList.Changed = true;
        }
    }
}
