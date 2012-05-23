using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public class Cubes : MetaObject
    {
        public static new string Type = MetaObjectType.Cubes.ToString();
        
        private static AttrNameType _ant_attrList = new AttrNameType { Name = "cubesList", Type = AttributeType.List };
        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { _ant_attrList};

        //Атрибуты
        private MetaObjectApp.Attribute elementList;
        //Свойства
        public List<int> CubesIds
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
        public Cubes(MetaObjectRepository repository)
            : base(repository)
        {
            this.TypeName = Cubes.Type;
            elementList = new MetaObjectApp.Attribute(this, _ant_attrList);
            attributes.Add(elementList);
        }
        //Методы
        public List<Cube> GetCubes()
        {
            List<int> list = elementList.Value as List<int>;
            List<Cube> cubes = new List<Cube>();
            foreach (int inx in list)
            {
                Cube cube = _repository.LoadMetaObject(inx) as Cube;
                cubes.Add(cube);
            }
            return cubes;
        }
        public void SetCubes(List<Cube> cubes)
        {
            List<int> list = new List<int>();

            foreach (Cube cube in cubes)
            {
                list.Add((int)cube.Id);
            }
            elementList.Value = list;
        }
        public void AddReglamentElement(Cube cube)
        {
            ((List<int>)elementList.Value).Add((int)cube.Id);
        }
        public void RemoveReglamentElement(Cube cube)
        {
            ((List<int>)elementList.Value).Remove((int)cube.Id);
        }
    }
}
