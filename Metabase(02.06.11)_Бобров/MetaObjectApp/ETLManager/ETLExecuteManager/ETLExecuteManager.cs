using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ETLManager
{
    class ETLExecuteManager
    {
        EventDecryptor _eventDecryptor;

        public ETLExecuteManager()
        {
            _eventDecryptor = new EventDecryptor();
        }

        public void ProcessEvent(DataSourceEvent e)
        {
            Dictionary<string, string> pathToETLAndArgs = _eventDecryptor.DecryptEvent(e);
            ProcessStartInfo info = new ProcessStartInfo(pathToETLAndArgs["path"],pathToETLAndArgs["args"]);
            info.UseShellExecute = false;
            Process p = Process.Start(info);
            Console.WriteLine("Запускаем сборку ETL");
            p.Exited += new EventHandler(p_Exited);

        }

        void p_Exited(object sender, EventArgs e)
        {
            Console.WriteLine("ETL процесс завершился");
        }
    }
}
