using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;
using System.Data.SqlClient;

namespace ETLManager
{   
    /// <summary>
    /// Метаобъект Куб. Для поддержки многомероной модели данных.
    /// </summary>
    public class Cube : MetaObject
    {
        public static new string Type = MetaObjectType.Cube.ToString();

        private static AttrNameType _ant_attrCubeName = new AttrNameType { Name = "CubeName", Type = AttributeType.String };
        private static AttrNameType _ant_attrDimensionsList = new AttrNameType { Name = "DimensionsList", Type = AttributeType.List };
        private static AttrNameType _ant_attrСписокПоказателей = new AttrNameType { Name = "СписокПоказателей", Type = AttributeType.List };
        private static AttrNameType _ant_attrСпособыАгрегацииНаИзмерения = new AttrNameType { Name = "СпособыАгрегацииНаИзмерения", Type = AttributeType.List };

        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { 
            _ant_attrCubeName,
            _ant_attrDimensionsList,
            _ant_attrСписокПоказателей,
            _ant_attrСпособыАгрегацииНаИзмерения
        };

        //Атрибуты
        private MetaObjectApp.Attribute cubeName;
        private MetaObjectApp.Attribute dimensionsList;
        private MetaObjectApp.Attribute списокПоказателей;
        private MetaObjectApp.Attribute способыАгрегацииНаИзмерения;
        //Свойства
        public string CubeName
        {
            get
            {
                return cubeName.Value.ToString();
            }
            set
            {
                cubeName.Value = value;
            }
        }
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
        //Конструкторы
        public Cube(MetaObjectRepository repository)
            : base(repository)
        {
            this.TypeName = Cube.Type;

            cubeName = new MetaObjectApp.Attribute(this, _ant_attrCubeName);
            dimensionsList = new MetaObjectApp.Attribute(this, _ant_attrDimensionsList);
            списокПоказателей = new MetaObjectApp.Attribute(this, _ant_attrСписокПоказателей);
            способыАгрегацииНаИзмерения = new MetaObjectApp.Attribute(this, _ant_attrСпособыАгрегацииНаИзмерения);

            attributes.Add(cubeName);
            attributes.Add(dimensionsList);
            attributes.Add(списокПоказателей);
            attributes.Add(способыАгрегацииНаИзмерения);
        }
        //Методы
        List<Dimension> GetDimensions()
        {
            List<int> list = dimensionsList.Value as List<int>;
            List<Dimension> dimensions = new List<Dimension>();
            foreach (int inx in list)
            {
                Dimension remo = _repository.LoadMetaObject(inx) as Dimension;
                dimensions.Add(remo);
            }
            return dimensions;
        }
        public void SetDimensions(List<Dimension> dimensions)
        {
            List<int> list = new List<int>();

            foreach (Dimension remo in dimensions)
            {
                list.Add((int)remo.Id);
            }
            dimensionsList.Value = list;
        }
        public void AddDimension(Dimension dimension)
        {
            if (dimensionsList.Value == null)
                dimensionsList.Value = new List<int>();
            List<int> list = (List<int>)dimensionsList.Value;
            list.Add((int)dimension.Id);
            dimensionsList.Value = list;
        }
        public void RemoveDimension(Dimension dimension)
        {
            List<int> list = (List<int>)dimensionsList.Value;
            list.Remove((int)dimension.Id);
            dimensionsList.Value = list;
        }
        public int DimensionsCount()
        {
            List<int> list = (List<int>)dimensionsList.Value;
            if (list == null)
                return 0;
            return list.Count;
        }
        /// <summary>
        /// Создает таблицу фактов
        /// </summary>
        /// <returns></returns>
        public bool CreateFactsTable()
        {
            List<Dimension> dimList = GetDimensions();
            SqlCommand cmd = new SqlCommand();
            string partCmd = "";

            foreach (Dimension dim in dimList)
            {
                string fieldName = "id_" + dim.Name;
                partCmd += string.Format("[{0}] [bigint] NOT NULL,",fieldName);
            }
            
            cmd.CommandText = string.Format("CREATE TABLE [dbo].[{0}](" +
                                "[id] [bigint] IDENTITY(1,1) NOT NULL," +
                                partCmd +
                                "[value] [nchar](500))", CubeName);
            return _repository.ExecuteNonQuery(cmd) > 0;
        }
        /// <summary>
        /// Заполняет таблицу фактов
        /// </summary>
        /// <param name="data"></param>
        public void Insert(List<Dictionary<string,object>> data)
        {
            SqlCommand cmd = new SqlCommand();

            List<Dimension> dimList = GetDimensions();
            

            foreach (Dictionary<string, object> param in data)
            {
                string insert = string.Format("insert into [dbo].[{0}] (", CubeName);
                string values = "values (";

                string fieldName;
                foreach (Dimension dim in dimList)
                {
                    fieldName = "id_" + dim.Name;
                    insert += fieldName+",";
                    values += param[fieldName]+",";
                }
                fieldName = "value";
                insert += fieldName;
                values += param[fieldName];

                insert += ")";
                values += ")";

                cmd.CommandText = insert + values;

                int res = _repository.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// Возвращает названия полей таблицы фактов
        /// </summary>
        /// <returns></returns>
        public List<string> GetFactTableListFields()
        {
            List<string> res = new List<string>();
            List<Dimension> dimList = GetDimensions();
     
            foreach (Dimension dim in dimList)
            {
                string fieldName = "id_" + dim.Name;
                res.Add(fieldName);
            }
            return res;
        }
        public bool DropTable()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = string.Format("drop TABLE [dbo].[{0}]", CubeName);
            return _repository.ExecuteNonQuery(cmd) > 0;
        }
    }
}
