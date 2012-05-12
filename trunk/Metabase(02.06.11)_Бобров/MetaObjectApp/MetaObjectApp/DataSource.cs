using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MetaObjectApp
{
    class DataSource:MetaObject
    {
        public static new string Type = MetaObjectType.DataSource.ToString();
        public static new List<AttrNameType> Attributes = new List<AttrNameType>() {new AttrNameType {Name="source",Type= AttributeType.String} };

        public Attribute source;

        public DataSource()
        {
            source=new Attribute(this, "source", AttributeType.String, "");
        }

        public override bool CreateNew(SqlConnection connection, string StrIdentifier)
        {
            if (CreateMetaObjRecord(connection, StrIdentifier, DataSource.Type))//если создался новый метаобъект
            {
                //код для создания атрибутов в базе данных
                source.Create(connection);
                return true;
            }
            else
                return false;       
        }
        public override bool LoadFromDatabase(int id, SqlConnection connection)
        {
            if (base.LoadFromDatabase(id, connection))
            {
                source.Load(connection);
                return true;
            }
            else
                return false;
        }
        public override bool LoadFromDatabase(string strIdentifier, SqlConnection connection)
        {
            if (base.LoadFromDatabase(strIdentifier, connection))
            {
                source.Load(connection);
                return true;
            }
            else
                return false;
        }
        public override void SaveToDatabase(System.Data.SqlClient.SqlConnection connection)
        {
            base.SaveToDatabase(connection);
            source.Save(connection);
        }
    }
}
