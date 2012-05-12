using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MetaObjectApp
{
    /// <summary>
    /// Метаобъект
    /// </summary>
    public class MetaObject
    {
        protected long id;

        //статические члены
        public static List<AttrNameType> Attributes = new List<AttrNameType>();
        public static string Type = MetaObjectType.MetaObject.ToString();

        //свойства
        public long Id
        {
            get
            {
                return id;
            }
        }
        public string Identifier{get;set;}//строковый идентификатор (имя)
        
        //контсруктор
        public MetaObject()
        {}
        //методы
        /// <summary>
        /// Сохранение метаобъекта в базу
        /// </summary>
        /// <param name="connection"></param>
        public virtual void SaveToDatabase(SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand("UPDATE TMetaObjects SET stridentifier=@pStrIdentifier WHERE id_metaobject=@pId", connection);
            SqlParameter pStrIdentifier = new SqlParameter("@pStrIdentifier", this.Identifier);
            SqlParameter pId = new SqlParameter("@pId", Id);
            cmd.Parameters.Add(pStrIdentifier);
            cmd.Parameters.Add(pId);
            cmd.ExecuteNonQuery();
            //код для сохранения атрибутов в базе данных
            //ALTER
            //пока нет атрибутов
        }
        /// <summary>
        /// Загрузка из базы
        /// </summary>
        /// <param name="id"></param>
        /// <param name="connection"></param>
        public virtual bool LoadFromDatabase(int id, SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM TMetaObjects m WHERE m.id_metaobject=@id", connection);
            SqlParameter pId = new SqlParameter("@id", id);
            cmd.Parameters.Add(pId);
            SqlDataReader sr = cmd.ExecuteReader();
            if (sr.Read())
            {
                this.id = (long)sr["id_metaobject"];
                this.Identifier = sr["stridentifier"].ToString();
                //код для загрузки атрибутов
                return true;
            }
            else
                return false;
        }
        public virtual bool LoadFromDatabase(string strIdentifier, SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM TMetaObjects m WHERE m.stridentifier=@strIdentifier", connection);
            SqlParameter pStrIdentifier = new SqlParameter("@strIdentifier", strIdentifier);
            cmd.Parameters.Add(pStrIdentifier);
            SqlDataReader sr = cmd.ExecuteReader();
            if (sr.Read())
            {
                this.id = (long)sr["id_metaobject"];
                this.Identifier = sr["stridentifier"].ToString().Trim();
                //код для загрузки атрибутов
                return true;
            }
            else
                return false;
        }
        //обобщенный метод создания нового метаобъекта
        protected bool CreateMetaObjRecord(SqlConnection connection, string StrIdentifier,string typeName)
        {
            if (StrIdentifier == null)
                StrIdentifier = String.Empty;
            //создаем новую запись о метаобъекте
            SqlCommand cmd = new SqlCommand("CreateNewMetaObj @MOTypeName, @StrIdentifier=@pStrIdentifier", connection);
            SqlParameter pMOTypeName = new SqlParameter("@MOTypeName", typeName);
            SqlParameter pStrIdentifier = new SqlParameter("@pStrIdentifier", StrIdentifier);
            cmd.Parameters.Add(pMOTypeName);
            cmd.Parameters.Add(pStrIdentifier);
            SqlDataReader sr = cmd.ExecuteReader();
            sr.Read();
            this.id = Convert.ToInt64( sr[0]);
            if (this.id >= 0)
            {
                this.Identifier = StrIdentifier;
                return true;
            }
            else
                return false;
        }
        
        /// <summary>
        /// Создает новый объект в базе данных
        /// </summary>
        /// <param name="connection"></param>
        public virtual bool CreateNew(SqlConnection connection,string StrIdentifier)
        {
            if (CreateMetaObjRecord(connection, StrIdentifier, MetaObject.Type))//если создался новый метаобъект
            {
                //код для создания атрибутов в базе данных
                return true;
            }
            else
                return false;
        }
     
    }
}
