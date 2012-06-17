using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;
using System.Data.SqlClient;

namespace ETLManager
{
    public class ReglamentMetaObject : MetaObject
    {
        public static new string Type = MetaObjectType.Reglament.ToString();
        private static AttrNameType _ant_attrList = new AttrNameType { Name = "elementList", Type = AttributeType.List };
        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { _ant_attrList};

        //Атрибуты
        public MetaObjectApp.Attribute elementList;

        public ReglamentMetaObject(MetaObjectRepository repository)
            : base(repository)
        {
            this.TypeName = ReglamentMetaObject.Type;
            elementList = new MetaObjectApp.Attribute(this, _ant_attrList);
            attributes.Add(elementList);
        }

        public List<ReglamentElementMetaObject> GetReglamentElements()
        {
            List<int> list = elementList.Value as List<int>;
            List<ReglamentElementMetaObject> reglamentElements = new List<ReglamentElementMetaObject>();
            foreach (int inx in list)
            {
                ReglamentElementMetaObject remo = _repository.LoadMetaObject(inx) as ReglamentElementMetaObject;
                reglamentElements.Add(remo);
            }
            return reglamentElements;
        }
        public void SetReglamentElements(List<ReglamentElementMetaObject> reglamentElements)
        {
            List<int> list = new List<int>();
     
            foreach (ReglamentElementMetaObject remo in reglamentElements)
            {
                list.Add((int)remo.Id);
            }
            elementList.Value = list;
        }
        public void AddReglamentElement(ReglamentElementMetaObject remo)
        {
            if (elementList.Value == null)
                elementList.Value = new List<int>();
            List<int> list =(List<int>) elementList.Value;
            list.Add((int)remo.Id);
            elementList.Value = list;
        }
        public void RemoveReglamentElement(ReglamentElementMetaObject remo)
        {
            List<int> list = (List<int>)elementList.Value;
            list.Remove((int)remo.Id);
            elementList.Value = list;
        }
    }
}
