using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Data.OleDb;
using System.IO;
using IDatasourceMonitorNS;

namespace DataSourceCheckerPlugin
{
    public class DataSourceCheckerPlugin : IDatasourceMonitor
    {
        #region Члены IDatasourceMonitor

        public string Name
        {
            get { return "DataSourceCheckerPlugin"; }
        }

        public string DataSourceName
        {
            get { return "Росстат. Население."; }
        }

        public string controlSum()
        {
            Console.WriteLine("\nПроверка источника...");
            Download();
            string sum = ReadFile();
            DeleteFile();
            return sum;
        }

        #endregion


        static string url = "http://www.gks.ru/free_doc/new_site/population/demo/demo14.xls";
        static string file = Directory.GetCurrentDirectory() + @"\demo14.xls";


        public static void Download()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile(new Uri(url), file);
        }

        /// <summary>
        /// Считает контрольную сумму
        /// </summary>
        /// <returns></returns>
        public static string ReadFile()
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + file + ";   Extended Properties=Excel 12.0 xml";
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select * from  [Лист1$]";
            connection.Open();
            OleDbDataReader dr = cmd.ExecuteReader();
            int s = 0;
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    try
                    {

                        s += Convert.ToInt32(dr[i]);
                    }
                    catch { }
                }
            }
            connection.Close();
            return s.ToString();
        }

        public static void DeleteFile()
        {
            File.Delete(file);
        }

   
    }
}
