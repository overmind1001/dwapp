using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MetaObjectApp
{
    public class Attribute
    {
        //Поля
        protected long id;
        protected string name;
        protected AttributeType type;
        protected object value;

        protected bool changed;

        protected MetaObject owner;
        //Конструкторы
        public Attribute(MetaObject owner,string name, AttributeType type, object value)
        {
            this.id = 0;
            this.name = name;
            this.type = type;
            this.value = value;
            this.changed = false;
            this.owner = owner;
        }
        //Свойства
        string Name
        {
            get
            { return this.name; }
            set
            {
                this.name = value;
            }

        }
        AttributeType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
                this.changed = true;
            }
        }
        bool Changed
        {
            get
            {
                return changed;
            }
            set
            {
                changed = value;
            }
        }

        //методы
        /// <summary>
        /// Создание экземпляра атрибута (при создании объекта)
        /// </summary>
        /// <param name="connection"></param>
        public void Create(SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand("CreateValue @metaObj_id=@pmetaObj_id, @attrName=@pattrName", connection);
            SqlParameter pmetaObjId = new SqlParameter("@pmetaObj_id", this.owner.Id);
            SqlParameter pattrName = new SqlParameter("@pattrName", this.Name);
           
            cmd.Parameters.Add(pmetaObjId);
            cmd.Parameters.Add(pattrName);
 
            SqlDataReader sr=cmd.ExecuteReader();
            if (sr.Read())
            {
                id = (long)sr[0];
            }
        }
        public void Save(SqlConnection connection)
        {
            if (!changed)
                return;

            SqlCommand cmd = new SqlCommand("SaveValue metaObj_id=@pmetaobject_id,@attr_id=@patr_id,@value=@pvalue", connection);
            SqlParameter pValue = new SqlParameter("@pvalue", this.value);
            SqlParameter pMetaobjectId = new SqlParameter("@pmetaobject_id", this.owner.Id);
            SqlParameter pAtrid = new SqlParameter("@patr_id", this.id);
            cmd.Parameters.Add(pValue);
            cmd.Parameters.Add(pMetaobjectId);
            cmd.Parameters.Add(pAtrid);

            switch (this.type)
            {
                case AttributeType.String:
                    cmd.CommandText = "UPDATE TStrValues SET value=@pvalue WHERE metaobject_id=@pmetaobject_id and atr_id=@patr_id";
                    break;
                case AttributeType.Bigint:
                    cmd.CommandText = "UPDATE TBigintValues SET value=@pvalue WHERE metaobject_id=@pmetaobject_id and atr_id=@patr_id";
                    break;
                case AttributeType.Double:
                    cmd.CommandText = "UPDATE TFloatValues SET value=@pvalue WHERE metaobject_id=@pmetaobject_id and atr_id=@patr_id";
                    break;
                case AttributeType.Id:
                    cmd.CommandText = "UPDATE TIdValues SET value=@pvalue WHERE metaobject_id=@pmetaobject_id and atr_id=@patr_id";
                    break;
                case AttributeType.List:
                    cmd.CommandText = "UPDATE TListValues SET value=@pvalue WHERE metaobject_id=@pmetaobject_id and atr_id=@patr_id";
                    break;
                case AttributeType.Binary://??
                    cmd.CommandText = "UPDATE TBinValues SET value=@pvalue WHERE metaobject_id=@pmetaobject_id and atr_id=@patr_id";
                    break;
            }
            int res = cmd.ExecuteNonQuery();

           
            changed = false;
        }
        public void Load(SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand("LoadValue  @metaObj_id=@pmetaobject_id, @attrName=@pattrName", connection);

            SqlParameter pMetaobjectId = new SqlParameter("@pmetaobject_id", this.owner.Id);
            SqlParameter pAttrName = new SqlParameter("@pattrName",this.Name);
        
            cmd.Parameters.Add(pMetaobjectId);
            cmd.Parameters.Add(pAttrName);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                this.value = sdr[0].ToString();
                this.id = (long)sdr[1];
            }
            switch (this.type)
            {
                case AttributeType.String:
                    this.value = this.value.ToString().Trim();
                    break;
                case AttributeType.Bigint:
                    break;
                case AttributeType.Double:
                    break;
                case AttributeType.Id:
                    break;
                case AttributeType.List:
                    break;
                case AttributeType.Binary://??
                    break;
            }
        }
    }
}
