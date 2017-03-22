using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLCLogin.Data
{
    static class Logger
    {
        private static string filePath = 
            AppDomain.CurrentDomain.GetData("DataDirectory") + @"\ErrorLog.txt";

        public static void LogException(Exception ex)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Date: " + DateTime.Now.ToString());
                    writer.WriteLine("Message: " + ex.Message);
                    writer.WriteLine("StackTrace: " + Environment.NewLine + ex.StackTrace);
                    writer.WriteLine(Environment.NewLine + 
                                     "-----------------------------------------------------------------------------" + 
                                     Environment.NewLine);
                }
            }
            catch { }       // How can you log an exception if the logger throws an exception
        }

    }
}
