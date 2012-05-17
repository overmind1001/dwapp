using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaObjectApp;
using ETLManager;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {//192.168.189.1
            MetaObjectRepository repository =
                new MetaObjectRepository("Data Source=localhost;Initial Catalog=MetaBase;connection timeout=15;Trusted_Connection=False;MultipleActiveResultSets=True;User ID=a; password=a");
            repository.AddFactory(new MetaObjectFactory());
            repository.AddFactory(new DataSourceFactory());
            repository.AddFactory(new ReglamentMetaObjectFactory());
            //создаем объект
            MetaObject mObj = repository.CreateNewMetaObject("Reglament", "r1");
            MetaObject mObj1 = repository.LoadMetaObject("r1");
            if (mObj1 != null)
                Console.WriteLine(mObj1.Id);
            else
                Console.WriteLine("Не удалось подключиться");

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
    }
}
