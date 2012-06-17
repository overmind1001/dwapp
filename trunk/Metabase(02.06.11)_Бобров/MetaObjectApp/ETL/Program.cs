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

namespace ETL
{
    class Program
    {
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
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                    Console.Write(dr[i] + "     ");
                Console.WriteLine();
            }
            connection.Close();
        }

        public static void DeleteFile()
        {
            File.Delete(file);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Началась загрузка файла. {0}",DateTime.Now);
            Download();
            Console.WriteLine("Загрузка файла завершена. {0}",DateTime.Now);
            Console.WriteLine("Чтение файла.");
            ReadFile();
            DeleteFile();
            Console.WriteLine("Файл удален.");
            Console.ReadKey();
        }
    }
}
