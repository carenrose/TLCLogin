/* Copyright (C) 2017 Brianna Williams
 *
 * This file is part of TLC Login.
 * 
 * TLC Login is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * TLC Login is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with TLC Login.  If not, see <http://www.gnu.org/licenses/>.
 */

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
