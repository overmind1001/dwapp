using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MetaObjectApp
{
    /// <summary>
    /// фабрика для инстанцирования объектов класса MetaObject
    /// </summary>
    public class MetaObjectFactory
    {
        //поля
        protected MetaObjectRepository _repository;

        //свойства
        /// <summary>
        /// Название инстанцируемого метаобъекта
        /// </summary>
        public virtual string Name
        {
            get
            { 
                return MetaObject.Type; 
            }
        }
        /// <summary>
        /// Список атрибутов инстанцируемого метаобъекта
        /// </summary>
        public virtual List<AttrNameType> Attributes
        {
            get
            {
                return MetaObject.Attributes;
            }
        }

        //конструктор
        public MetaObjectFactory(MetaObjectRepository repository)
        {
            _repository = repository;
        }

        //методы
        /// <summary>
        /// Инстанцирование метаобъекта
        /// </summary>
        /// <returns></returns>
        public virtual MetaObject CreateObject()
        {
            return new MetaObject(_repository);
        }

        /// <summary>
        /// Добавление типа инстанцируемого объекта в метабазу
        /// </summary>
        /// <param name="connection"></param>
        public void AddNewType(SqlConnection connection)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("AddNewType @typeName=@nameType", connection);
            SqlParameter pName = new SqlParameter("@nameType", Name);
            cmd.Parameters.Add(pName);
            result = cmd.ExecuteNonQuery();
            
            if (result > 0)//добавлен тип
            {
                foreach (AttrNameType attr in Attributes)
                {
                    AddNewAttribute(connection,attr, Name);
                }
            }
        }
        /// <summary>
        /// Добавление нового атрибута в метабазу
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="attr"></param>
        /// <param name="typeName"></param>
        protected void AddNewAttribute(SqlConnection connection,AttrNameType attr, string typeName)
        {
            SqlCommand cmd = new SqlCommand("AddNewAttr @attrName=@pattrName, @attrType=@pattrType, @typeName=@ptypeName", connection);
            SqlParameter pattrName = new SqlParameter("@pattrName", attr.Name);
            SqlParameter pattrType = new SqlParameter("@pattrType", attr.Type.ToString());
            SqlParameter ptypeName = new SqlParameter("@ptypeName", typeName);
            cmd.Parameters.Add(pattrName);
            cmd.Parameters.Add(pattrType);
            cmd.Parameters.Add(ptypeName);
            cmd.ExecuteNonQuery();
        }
    }
}
