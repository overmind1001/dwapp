using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    public class ETL : MetaObject
    {
        public static new string Type = MetaObjectType.ETL.ToString();

        private static AttrNameType _ant_attrAssemblyPath = new AttrNameType { Name = "assemblyPath", Type = AttributeType.String };
        private static AttrNameType _ant_attrDataSourceId = new AttrNameType { Name = "dataSourceId", Type = AttributeType.Id };
        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { 
            _ant_attrAssemblyPath,
            _ant_attrDataSourceId
        };

        //Атрибуты
        private MetaObjectApp.Attribute assemblyPath;
        private MetaObjectApp.Attribute dataSourceId;
      

        //свойства
        public string AssemblyPath
        {
            get
            {
                return assemblyPath.Value.ToString();
            }
            set
            {
                assemblyPath.Value = value;
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
        
        //Конструктор
        public ETL(MetaObjectRepository repository)
            : base(repository)
        {
            this.TypeName = ETL.Type;

            assemblyPath = new MetaObjectApp.Attribute(this, _ant_attrAssemblyPath);
            dataSourceId = new MetaObjectApp.Attribute(this, _ant_attrDataSourceId);

            attributes.Add(assemblyPath);
            attributes.Add(dataSourceId);
        }

        public DataSource getDataSource()
        {
            DataSource ds = _repository.LoadMetaObject((int)DataSourceId) as DataSource;
            return ds;
        }
    }
}
