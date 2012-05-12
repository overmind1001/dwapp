using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;
using System.Data.SqlClient;

namespace ETLManager
{
    class ReglamentMetaObject : MetaObject
    {
        public const string ELEMENT_LIST = "elementList";


        public static new string Type = MetaObjectType.Reglament.ToString();
        public static new List<AttrNameType> Attributes = new List<AttrNameType>() {
            new AttrNameType {Name=ELEMENT_LIST,Type= AttributeType.List} 
        };

        public MetaObjectApp.Attribute elementList;

        public ReglamentMetaObject()
        {
            elementList = new MetaObjectApp.Attribute(this, ELEMENT_LIST, AttributeType.List, "");
        }

        public override bool CreateNew(SqlConnection connection, string StrIdentifier)
        {
            if (CreateMetaObjRecord(connection, StrIdentifier, ReglamentMetaObject.Type))//если создался новый метаобъект
            {
                //код для создания атрибутов в базе данных
                elementList.Create(connection);
                return true;
            }
            else
                return false;       
        }
        public override bool LoadFromDatabase(int id, SqlConnection connection)
        {
            if (base.LoadFromDatabase(id, connection))
            {
                elementList.Load(connection);
                return true;
            }
            else
                return false;
        }
        public override bool LoadFromDatabase(string strIdentifier, SqlConnection connection)
        {
            if (base.LoadFromDatabase(strIdentifier, connection))
            {
                elementList.Load(connection);
                return true;
            }
            else
                return false;
        }
        public override void SaveToDatabase(System.Data.SqlClient.SqlConnection connection)
        {
            base.SaveToDatabase(connection);
            elementList.Save(connection);
        }


        internal List<ReglamentElementMetaObject> GetReglamentElements()
        {
            throw new NotImplementedException();
        }
    }
}
