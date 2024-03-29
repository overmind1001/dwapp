﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Threading;

namespace MetaObjectApp
{
    public class MetaObjectRepository
    {
        List<MetaObject> cache;
        string connectionString;
        SqlConnection connection;
        List<MetaObjectFactory> factories;

        //блокировки

        //консруктор
        public MetaObjectRepository(string connectionString)
        {
            cache = new List<MetaObject>();
            factories = new List<MetaObjectFactory>();
            this.connectionString=connectionString;
            connection = new SqlConnection(connectionString);
        }
        //методы

        /// <summary>
        /// Добавляет фабрику
        /// </summary>
        /// <param name="factory"></param>
        public void AddFactory(MetaObjectFactory factory)
        {
            connection.Open();
            factories.Add(factory);
            factory.AddNewType(connection);
            connection.Close();
        }
        /// <summary>
        /// Создание нового метаобъекта заданного типа
        /// </summary>
        /// <param name="name">имя типа</param>
        /// <returns></returns>
        public MetaObject CreateNewMetaObject(string nameType,string StrIdentifier)
        {
            lock (this)
            {
                MetaObjectFactory factory = factories.Find(f => { return f.Name == nameType; });
                if (factory == null)
                    return null;
                MetaObject mObj = factory.CreateObject();//создаем объект в памяти
                connection.Open();
                bool moCreated = mObj.CreateNew(connection, StrIdentifier);//создаем его в базе
                connection.Close();
                if (!moCreated)//если не создался(такой идентификатор уже занят), то возвращаем пустую ссылку
                    return null;
                cache.Add(mObj);
                return mObj;
            }
        }
        public MetaObject CreateNewMetaObject(MetaObjectType type, string StrIdentifier)
        {
            return CreateNewMetaObject(type.ToString(), StrIdentifier);
        }
        /// <summary>
        /// Загрузка существующего метаобъекта по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MetaObject LoadMetaObject(int id)
        {//если в кэше есть объект, то сразу кинуть ссылку
            lock (this)
            {
                MetaObject mObj = cache.Find(m => { return (m.Id == id); });
                if (mObj == null)//нет в кэше
                {
                    connection.Open();
                    SqlCommand cmdGetTypeName = new SqlCommand("SELECT mot.name FROM TMetaObjects mo,TMetaObjectTypes mot WHERE mo.type_id=mot.id_type AND mo.id_metaobject=@id", connection);
                    SqlParameter pId = new SqlParameter("@id", id);
                    cmdGetTypeName.Parameters.Add(pId);
                    SqlDataReader sr = cmdGetTypeName.ExecuteReader();
                    if (!sr.Read())
                    {
                        connection.Close();
                        return null;
                    }
                    string typeName = sr["name"].ToString().Trim();

                    MetaObjectFactory factory = factories.Find(f => { return f.Name == typeName; });
                    if (factory == null)
                    {
                        connection.Close();
                        return null;
                    }
                    mObj = factory.CreateObject();
                    mObj.LoadFromDatabase(id, connection);
                    cache.Add(mObj);
                    connection.Close();
                }
                return mObj;
            }
        }
        /// <summary>
        /// Загрузка существующего метаобъекта по строковому идентификатору
        /// </summary>
        /// <param name="strIdentifier"></param>
        /// <returns></returns>
        public MetaObject LoadMetaObject(string strIdentifier)
        {//если в кэше есть объект, то сразу кинуть ссылку
            lock (this)
            {
                MetaObject mObj = cache.Find(m => { return (m.Identifier == strIdentifier); });
                if (mObj == null)//нет в кэше
                {
                    connection.Open();
                    SqlCommand cmdGetTypeName = new SqlCommand("SELECT mot.name FROM TMetaObjects mo,TMetaObjectTypes mot WHERE mo.type_id=mot.id_type AND mo.stridentifier=@pStrIdentifier", connection);
                    SqlParameter pStrIdentifier = new SqlParameter("@pStrIdentifier", strIdentifier);
                    cmdGetTypeName.Parameters.Add(pStrIdentifier);
                    SqlDataReader sr = cmdGetTypeName.ExecuteReader();
                    if (!sr.Read())//нет такого метаобъекта
                    {
                        connection.Close();
                        return null;
                    }
                    string typeName = sr["name"].ToString().Trim();

                    MetaObjectFactory factory = factories.Find(f => { return f.Name == typeName; });
                    if (factory == null)
                    {
                        connection.Close();
                        return null;
                    }
                    mObj = factory.CreateObject();
                    bool moLoaded = mObj.LoadFromDatabase(strIdentifier, connection);
                    connection.Close();
                    if (!moLoaded)
                        return null;
                    cache.Add(mObj);
                }
                return mObj;
            }
        }
        /// <summary>
        /// Сохранение метаобъекта
        /// </summary>
        /// <param name="mo"></param>
        public void Save(MetaObject mo)
        {
            lock (this)
            {

                if (cache.Contains(mo))
                {
                    connection.Open();
                    mo.SaveToDatabase(connection);
                    connection.Close();
                }
            }
        }   
        /// <summary>
        /// Сбросить кэш в базу
        /// </summary>
        public void SaveAll()
        {
            lock (this)
            {
                connection.Open();
                foreach (MetaObject mo in cache)
                {
                    mo.SaveToDatabase(connection);
                }
                connection.Close();
            }
        }
        /// <summary>
        /// Очищает кэш
        /// </summary>
        public void ClearCache()
        {
            cache.Clear();
        }
        /// <summary>
        /// Создание нового либо загрузка существующего метаобъекта.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="StrIdentifier"></param>
        /// <returns></returns>
        public MetaObject CreateOrLoadMetaObject(MetaObjectType type, string StrIdentifier)
        {
            MetaObject mo = CreateNewMetaObject(type, StrIdentifier);
            if (mo == null)
                mo = LoadMetaObject(StrIdentifier);
            return mo;
        }
        /// <summary>
        /// Выполнение sql-запроса
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(SqlCommand cmd)
        {
            int res;
            cmd.Connection = connection;
            try
            {
                connection.Open();
                res = cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
            return res;
        }
        
    }
}
