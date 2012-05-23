using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{//?????????????????????????????????????????? список показателей, впособы агрегации в измерениях
    public class Cube : MetaObject
    {
        public static new string Type = MetaObjectType.Cube.ToString();

        private static AttrNameType _ant_attrDimensionsList = new AttrNameType { Name = "DimensionsList", Type = AttributeType.List };
        private static AttrNameType _ant_attrСписокПоказателей = new AttrNameType { Name = "СписокПоказателей", Type = AttributeType.List };
        private static AttrNameType _ant_attrСпособыАгрегацииНаИзмерения = new AttrNameType { Name = "СпособыАгрегацииНаИзмерения", Type = AttributeType.List };

        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { 
            _ant_attrDimensionsList,
            _ant_attrСписокПоказателей,
            _ant_attrСпособыАгрегацииНаИзмерения
        };

        //Атрибуты
        private MetaObjectApp.Attribute dimensionsList;
        private MetaObjectApp.Attribute списокПоказателей;
        private MetaObjectApp.Attribute способыАгрегацииНаИзмерения;
        //Свойства
        public List<int> DimensionsList
        {
            get
            {
                return dimensionsList.Value as List<int>;
            }
            set
            {
                dimensionsList.Value = value;
            }
        }
        public List<int> СписокПоказателей
        {
            get
            {
                return списокПоказателей.Value as List<int>;
            }
            set
            {
                списокПоказателей.Value = value;
            }
        }
        public List<int> СпособыАгрегацииНаИзмерения
        {
            get
            {
                return способыАгрегацииНаИзмерения.Value as List<int>;
            }
            set
            {
                способыАгрегацииНаИзмерения.Value = value;
            }
        }

        public Cube(MetaObjectRepository repository)
            : base(repository)
        {
            this.TypeName = Cube.Type;

            dimensionsList = new MetaObjectApp.Attribute(this, _ant_attrDimensionsList);
            списокПоказателей = new MetaObjectApp.Attribute(this, _ant_attrСписокПоказателей);
            способыАгрегацииНаИзмерения = new MetaObjectApp.Attribute(this, _ant_attrСпособыАгрегацииНаИзмерения);

            attributes.Add(dimensionsList);
            attributes.Add(списокПоказателей);
            attributes.Add(способыАгрегацииНаИзмерения);
        }
    }
}
