using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data;
using System.Net;
using System.Diagnostics;
using System.IO;
using MetaObjectApp;
using ETLManager;

namespace ETL
{
    class Program
    {
        static MetaObjectRepository repository;

        static string url = "http://www.gks.ru/free_doc/new_site/population/demo/demo14.xls";
        static string file = Directory.GetCurrentDirectory() + @"\demo14.xls";

        public static void ODBC()
        {
            OdbcConnection oConn = new OdbcConnection();
            oConn.ConnectionString = @"Driver={Microsoft Excel Driver (*.xlsx)};DriverId=790;Dbq=D:\Универ\4 курс\2 семестр\Выпускная работа бакалавра\Metabase(02.06.11)_Бобров\Metabase(02.06.11)_Бобров\Тренировки.xlsx;DefaultDir=D:\Универ\4 курс\2 семестр\Выпускная работа бакалавра\Metabase(02.06.11)_Бобров\Metabase(02.06.11)_Бобров\;";
            OdbcCommand oCmd = oConn.CreateCommand();
            oCmd.CommandText = "SELECT * FROM [Sheet1$]";
            DataTable table = new DataTable();
            oConn.Open();
            table.Load(oCmd.ExecuteReader());
        }

        public static void Download()
        { 
            WebClient webClient = new WebClient();
            webClient.DownloadFile(new Uri(url), file);
        }

        public static void ReadFile()
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source="+file+";   Extended Properties=Excel 12.0 xml";
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select * from  [Лист1$]";
            connection.Open();
            OleDbDataReader dr = cmd.ExecuteReader();

            DataTable t = new DataTable();
            t.Load(dr);

            Dictionary<int, object> years = new Dictionary<int, object>();
            Dictionary<int, object> age = new Dictionary<int, object>();
            Dictionary<int, object> place = new Dictionary<int, object>();

            //заполняем измерения
            int i, j;

            //годы
            i = 2;
            for (j = 1; j < t.Columns.Count; j++)
            {
                years.Add(j-1, t.Rows[i].Field<object>(j));
            }

            //возрастные группы
            j = 0;
            for (i = 5; ; i++)
            {
                if (t.Rows[i].Field<object>(j).ToString().StartsWith("Из"))
                    break;
                age.Add(i-5, "'"+t.Rows[i].Field<object>(j).ToString()+"'");
            }

            //заполняем местность
            place.Add(0, "'Городское население'");
            place.Add(1, "'Сельское население'");

            //ищем городское население
            int startI = 0;
            while (true)
            {
                if(t.Rows[startI].Field<object>(0)!=null)
                {
                    if(t.Rows[startI].Field<object>(0).ToString().StartsWith("Городское население"))
                        break;
                }
                startI++;

                if (startI > 10000)
                    throw new Exception("Городское население не найдено!");
            }
            startI += 2;//пропускаем строку
            //читаем городское население
            //вывод на консоль
            //for (i = 0; i < age.Count; i++)
            //{
            //    for (j = 0; j < years.Count; j++)
            //    {
            //        Console.Write("{0}  ", t.Rows[startI + i].Field<object>(j + 1));
            //    }
            //    Console.WriteLine();
            //}
            repository.AddFactory(new CubeFactory(repository));
            repository.AddFactory(new DimensionFactory(repository));


            Cube cube = repository.CreateOrLoadMetaObject(MetaObjectType.Cube, "Куб.Население") as Cube;
            cube.CubeName = "Население";

            Dimension dimYears = repository.CreateOrLoadMetaObject(MetaObjectType.Dimension, "Изм.Годы") as Dimension;
            dimYears.Name = "Годы";
            dimYears.Table = "Годы";
            repository.Save(dimYears);
            Dimension dimAge = repository.CreateOrLoadMetaObject(MetaObjectType.Dimension, "Изм.ВозрастГр") as Dimension;
            dimAge.Name = "Возраст";
            dimAge.Table = "Возраст";
            repository.Save(dimAge);
            Dimension dimPlace = repository.CreateOrLoadMetaObject(MetaObjectType.Dimension, "Изм.Место") as Dimension;
            dimPlace.Name = "Место";
            dimPlace.Table = "Место";
            repository.Save(dimPlace);

            if (cube.DimensionsCount() == 0)
            {
                cube.AddDimension(dimYears);
                cube.AddDimension(dimAge);
                cube.AddDimension(dimPlace);
            }
            repository.Save(cube);
            
            //заполняем измерения
            try
            {
                dimYears.DropTable();
            }
            catch { }
            bool res = dimYears.CreateTable();
            dimYears.Insert(years);

            try
            {
                dimAge.DropTable();
            }
            catch { }
            res = dimAge.CreateTable();
            dimAge.Insert(age);

            try
            {
                dimPlace.DropTable();
            }
            catch { }
            res = dimPlace.CreateTable();
            dimPlace.Insert(place);

            //заполнение таблицы фактов
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            List<string> fieldList = cube.GetFactTableListFields();

            for (i = 0; i < age.Count; i++)
            {
                for (j = 0; j < years.Count; j++)
                {
                    //Console.WriteLine("Координаты: {0} {1} {2} : {3}", years[j], age[i], place[0], t.Rows[startI + i].Field<object>(j + 1));
                    Console.WriteLine("Координаты: {0} {1} {2} : {3}", j, i,0, t.Rows[startI + i].Field<object>(j + 1));
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    row.Add(fieldList[0], j);
                    row.Add(fieldList[1], i);
                    row.Add(fieldList[2], 0);
                    row.Add("value", t.Rows[startI + i].Field<object>(j + 1));
                    list.Add(row);
                }
            }

            //сельское население
            while (true)
            {
                if (t.Rows[startI].Field<object>(0) != null)
                {
                    if (t.Rows[startI].Field<object>(0).ToString().StartsWith("Сельское население"))
                        break;
                }
                startI++;

                if (startI > 10000)
                    throw new Exception("Сельское население не найдено!");
            }
            startI += 2;//пропускаем строку
            //читаем сельское население
            //вывод на консоль
            //for (i = 0; i < age.Count; i++)
            //{
            //    for (j = 0; j < years.Count; j++)
            //    {
            //        Console.Write("{0}  ", t.Rows[startI + i].Field<object>(j + 1));
            //    }
            //    Console.WriteLine();
            //}
            for (i = 0; i < age.Count; i++)
            {
                for (j = 0; j < years.Count; j++)
                {
                    //Console.WriteLine("Координаты: {0} {1} {2} : {3}", years[j], age[i], place[1], t.Rows[startI + i].Field<object>(j + 1));
                    Console.WriteLine("Координаты: {0} {1} {2} : {3}", j, i, 1, t.Rows[startI + i].Field<object>(j + 1));
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    row.Add(fieldList[0], j);
                    row.Add(fieldList[1], i);
                    row.Add(fieldList[2], 1);
                    row.Add("value", t.Rows[startI + i].Field<object>(j + 1));
                    list.Add(row);
                }
            }

            try
            {
                cube.DropTable();
            }
            catch { }
            res = cube.CreateFactsTable();
            cube.Insert(list);

            



            //for (i = 0; i < t.Rows.Count;i++ )
            //{
            //    for (j = 0; j < t.Columns.Count; j++)
            //    {
            //        Console.Write("{0}  ", t.Rows[i].Field<object>(j));
            //    }
            //    Console.WriteLine();
            //}
            //int j = 0;
            //while (dr.Read())
            //{
            //    j++;
            //    Console.Write("{0} строка:", j);
            //    for (int i = 0; i < dr.FieldCount; i++)
            //        Console.Write(dr[i] + "     ");
            //    Console.WriteLine();

                
            //}
            connection.Close();
        }

        public static void DeleteFile()
        {
            File.Delete(file);
        }

        static void Main(string[] args)
        {
            string connectionString="";// = "Data Source=localhost;Initial Catalog=MetaBase;connection timeout=15;Trusted_Connection=True;MultipleActiveResultSets=True;"; //args[0];
            //connectionString = args[0];

            Console.WriteLine("Говорит сборка етл");
            
            foreach (string s in args)
            {
                connectionString += s + " ";
            }

            repository = new MetaObjectRepository(connectionString);

            Console.WriteLine("Началась загрузка файла. {0}", DateTime.Now);
            Download();
            Console.WriteLine("Загрузка файла завершена. {0}", DateTime.Now);
            Console.WriteLine("Чтение файла.");
            ReadFile();
            DeleteFile();
            Console.WriteLine("Файл удален.");
            Console.ReadKey();
        }
    }
}


//select  distinct

//dbo.Годы.value as Годы,
//dbo.Возраст.value as Возраст,
//dbo.Место.value as Место,
//dbo.Население.value as Значение

// from dbo.Население,dbo.Место,dbo.Возраст,dbo.Годы
 
// where dbo.Население.id_Место=dbo.Место.id and dbo.Население.id_Возраст=dbo.Возраст.id and dbo.Население.id_Годы = dbo.Годы.id
// order by Место,Годы, Возраст