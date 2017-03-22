using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace TLCLogin.Data
{
    public static class LoginDB
    {
        private const string ConnectionString =
            @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\TLCLoginDatabase.mdb";
        
        

        public static OleDbConnection GetConnection()
        {
            return new OleDbConnection(ConnectionString);
        }
                
        
        /// <summary>
        /// Logs and throws an exception with more readable message
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <param name="message">The end of a sentence - "There was an error _____." Do not include period.</param>
        public static void PrettifyAndLogException(Exception ex, string message, bool forStudentView = true)
        {
            message = "There was an error " + message + ".\n";
            if (forStudentView)
                message += "Please show this message to the tutor coordinator or a tutor.";
            else
                message += "This message has been logged.  If the problem persists, seek assistance.";

            Logger.LogException(ex); 
            throw new Exception(message, ex);
        }
        
    }
}
