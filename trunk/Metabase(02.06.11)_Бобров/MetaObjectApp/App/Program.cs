using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;
using ETLManager;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ETLManager;

namespace App
{
    class Program
    {
        static MetaObjectRepository repository;

        static void Main(string[] args)
        {//192.168.189.1
            repository =
                //new MetaObjectRepository("Data Source=localhost;Initial Catalog=MetaBase;connection timeout=15;Trusted_Connection=False;MultipleActiveResultSets=True;User ID=a; password=a");
                new MetaObjectRepository("Data Source=localhost;Initial Catalog=MetaBase;connection timeout=15;Trusted_Connection=True;MultipleActiveResultSets=True;");
            repository.AddFactory(new MetaObjectFactory(repository));
            repository.AddFactory(new DataSourceFactory(repository));
            repository.AddFactory(new ReglamentMetaObjectFactory(repository));
            repository.AddFactory(new ReglamentElementMetaObjectFactory(repository));
            repository.AddFactory(new TestMetaObjectFactory(repository));
            repository.AddFactory(new CubeFactory(repository));
            repository.AddFactory(new CubesFactory(repository));
            repository.AddFactory(new DataSourceEventFactory(repository));
            repository.AddFactory(new DataSourceEventsFactory(repository));
            repository.AddFactory(new DimensionFactory(repository));
            repository.AddFactory(new ETLFactory(repository));
            repository.AddFactory(new ETLsFactory(repository));

            test();
            init();

            ETLManager.ETLManager etlManager = new ETLManager.ETLManager(repository);
            etlManager.Start();
       
            Console.ReadKey();
            //DataSource dsss = (DataSource)repository.LoadMetaObject("data4");
            //DataSource dsObj = (DataSource) repository.CreateNewMetaObject(MetaObjectType.DataSource,"data4");
            //dsObj.source.Value = "example";
            //repository.Save(dsObj);
            //загружаем объект из базы
            //MetaObject mObj1 = repository.LoadMetaObject(1);
            //а теперь из кэша
            //mObj1 = repository.LoadMetaObject(1);
            //сохраняем
            //repository.Save(mObj1);

            //сохранения кэша в базу
            //repository.SaveAll();


            //MetaObject mo3= repository.LoadMetaObject(13);
            //mo3.Identifier = "new";
            //repository.Save(mo3);

            //repository.SaveAll();
        }

        static void test()
        {
            //создаем объект
            MetaObject mObj = repository.CreateNewMetaObject("TestMetaObject", "test11");

            mObj = repository.LoadMetaObject("test11");
            if (mObj != null)
            {
                Console.WriteLine(mObj.Id);
                TestMetaObject tmo = (TestMetaObject)mObj;

                tmo.attrString.Value = "Строка";
                tmo.attrBigint.Value = 9999992;
                tmo.attrDouble.Value = 3.141592;
                tmo.attrId.Value = (Int64)5;

                List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                tmo.attrList.Value = list;

                byte[] buf = new byte[1024];
                MemoryStream ms = new MemoryStream(buf);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, list);
                tmo.attrBinary.Value = buf;

                repository.Save(mObj);

                //а теперь загрузим его и проверим
                tmo = null;
                repository.ClearCache();
                list = null;

                tmo = repository.LoadMetaObject("test11") as TestMetaObject;

                list = tmo.attrList.getListIds();

            }
            else
                Console.WriteLine("Не удалось подключиться");
        }

        static void init()
        {
            //события
            DataSourceEvents dses = repository.CreateOrLoadMetaObject(MetaObjectType.DataSourceEvents,"DataSourceEvents") as DataSourceEvents;
            repository.Save(dses);
            //ETL
            ETLs etls = repository.CreateOrLoadMetaObject(MetaObjectType.ETLs, "ETLs") as ETLs;
            ETL etl = repository.CreateOrLoadMetaObject(MetaObjectType.ETL, "etl1") as ETL;
            etl.AssemblyPath=@"D:\Универ\4 курс\2 семестр\Выпускная работа бакалавра\Metabase(02.06.11)_Бобров\Metabase(02.06.11)_Бобров\1.exe";
            repository.Save(etl);

            etls.AddETL(etl);
            
            repository.Save(etls);
            

            //источник данных
            DataSource ds = repository.CreateOrLoadMetaObject(MetaObjectType.DataSource, "ds1") as DataSource;
            ds.DataSourceName="dsname";
            ds.DataSourceType="type";
            ds.Url="url";
            ds.SetETL(etl);
            repository.Save(ds);

            //создаем расписание и задание
            ReglamentMetaObject rmo = repository.CreateOrLoadMetaObject(MetaObjectType.Reglament, "Reglament") as ReglamentMetaObject;
            ReglamentElementMetaObject remo = repository.CreateOrLoadMetaObject(MetaObjectType.ReglamentElement, "ReglamentElement"+new Random().Next()) as ReglamentElementMetaObject;
            rmo.AddReglamentElement(remo);
            remo.Enabled = true;
            remo.ReglamentElementType="ExecETL";
            remo.NextRunTime = DateTime.Parse("13:14 16.6.2012");
            remo.Period = TimeSpan.FromMinutes(5);
            remo.SetDataSource(ds);
            
            repository.Save(remo);
            repository.Save(rmo);

            
           
        }
    }
}
