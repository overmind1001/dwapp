using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace MetaObjectApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //MetaObjectRepository repository = new MetaObjectRepository("Data Source=192.168.150.3;Initial Catalog=MetaBase;Integrated Security=True;Trusted_Connection=yes;Server=localhost;connection timeout=15;MultipleActiveResultSets=True");
            
            MetaObjectRepository repository = 
                new MetaObjectRepository("Data Source=192.168.189.1;Initial Catalog=MetaBase;connection timeout=15;Trusted_Connection=False;MultipleActiveResultSets=True;User ID=a; password=a");
            repository.AddFactory(new MetaObjectFactory());
            repository.AddFactory(new DataSourceFactory());
            
            //создаем объект
            MetaObject mObj= repository.CreateNewMetaObject("MetaObject","aaa1");
            MetaObject mObj1=repository.LoadMetaObject("aaa1");
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
