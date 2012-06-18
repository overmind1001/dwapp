using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;
using System.Data.SqlClient;

namespace ETLManager
{
    public class Dimension : MetaObject
    {
        public static new string Type = MetaObjectType.Dimension.ToString();

        private static AttrNameType _ant_attrName = new AttrNameType { Name = "name", Type = AttributeType.String };
        private static AttrNameType _ant_attrTable = new AttrNameType { Name = "table", Type = AttributeType.String };

        public static new List<AttrNameType> Attributes = new List<AttrNameType>() { 
            _ant_attrName,
            _ant_attrTable
        };

        //Атрибуты
        private MetaObjectApp.Attribute name;
        private MetaObjectApp.Attribute table;
        //Свойства
        public string Name
        {
            get
            {
                return name.Value.ToString();
            }
            set
            {
                name.Value = value;
            }
        }
        public string Table
        {
            get
            {
                return table.Value.ToString();
            }
            set
            {
                table.Value = value;
            }
        }

        public Dimension(MetaObjectRepository repository)
            : base(repository)
        {
            this.TypeName = Dimension.Type;

            name = new MetaObjectApp.Attribute(this, _ant_attrName);
            table = new MetaObjectApp.Attribute(this, _ant_attrTable);

            attributes.Add(name);
            attributes.Add(table);
        }

        public bool CreateTable()
        {
            SqlCommand cmd = new SqlCommand();
            
            cmd.CommandText = string.Format("CREATE TABLE [dbo].[{0}](" +
                                "[id] [bigint] NOT NULL," +
                                "[value] [nchar](500)," +
                                "[parent_id] [bigint])", Table);
            return _repository.ExecuteNonQuery(cmd) > 0;
            
        }

        public bool DropTable()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = string.Format("drop TABLE [dbo].[{0}]", Table);
            return _repository.ExecuteNonQuery(cmd)>0;
        }

        public void Insert(Dictionary<int, object> data)
        {
            SqlCommand cmd = new SqlCommand();

            foreach (KeyValuePair<int, object> pair in data)
            {
                Console.WriteLine("Insert: {0}, {1}",
                pair.Key,
                pair.Value);

                cmd.CommandText = string.Format("insert into [dbo].[{0}] (id,value) values ({1},{2})", Table,pair.Key,pair.Value);
                int res = _repository.ExecuteNonQuery(cmd);
            }
        }
    }
}
