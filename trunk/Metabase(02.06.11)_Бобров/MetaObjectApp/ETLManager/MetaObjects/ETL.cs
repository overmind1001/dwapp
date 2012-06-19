using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;

namespace ETLManager
{
    /// <summary>
    /// Метаобъект ETL
    /// </summary>
    public class ETL : MetaObject
    {
        //Поля
        public static new string Type = MetaObjectType.ETL.ToString();

        private static AttrNameType _ant_attrAssemblyPath = new AttrNameType { Name = "assemblyPath", Type = AttributeType.String };
        private static AttrNameType _ant_attrAssemblyArgs = new AttrNameType { Name = "assemblyArgs", Type = AttributeType.String };
        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { 
            _ant_attrAssemblyPath,
            _ant_attrAssemblyArgs
        };

        //Атрибуты
        private MetaObjectApp.Attribute assemblyPath;
        private MetaObjectApp.Attribute assemblyArgs;

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
        public string AssemblyArgs
        {
            get
            {
                return assemblyArgs.Value.ToString();
            }
            set
            {
                assemblyArgs.Value = value;
            }
        }

        //Конструктор
        public ETL(MetaObjectRepository repository)
            : base(repository)
        {
            this.TypeName = ETL.Type;

            assemblyPath = new MetaObjectApp.Attribute(this, _ant_attrAssemblyPath);
            assemblyArgs = new MetaObjectApp.Attribute(this, _ant_attrAssemblyArgs);

            attributes.Add(assemblyPath);
            attributes.Add(assemblyArgs);
        }
    }
}
