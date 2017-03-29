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
