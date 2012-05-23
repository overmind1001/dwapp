using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;
using ETLManager;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {//192.168.189.1
            Console.WriteLine(Cubes.Type);
            MetaObjectRepository repository =
                new MetaObjectRepository("Data Source=localhost;Initial Catalog=MetaBase;connection timeout=15;Trusted_Connection=False;MultipleActiveResultSets=True;User ID=a; password=a");
            repository.AddFactory(new MetaObjectFactory(repository));
            repository.AddFactory(new DataSourceFactory(repository));
            repository.AddFactory(new ReglamentMetaObjectFactory(repository));
            repository.AddFactory(new ReglamentElementMetaObjectFactory(repository));
            repository.AddFactory(new TestMetaObjectFactory(repository));

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
                tmo.attrId.Value = 5;

                List<int> list = new List<int>{1,2,3,4,5,6,7,8,9,10};
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

       
         //   Console.ReadKey();
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
    }
}
