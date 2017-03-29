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
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;

namespace TLCLogin.Data
{
    static class WindowsAuthenticator
    {
        /// <summary>
        /// Checks username and password against Domain (or Local Machine if not on Domain)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>If the username and password are correct</returns>
        public static bool Authenticate(string username, string password)
        {
            bool isValid = false;

            // test login on domain
            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                {
                    isValid = context.ValidateCredentials(username, password);
                }
            }
            catch (Exception ex)
            {
                // test login on local machine
                if (!isValid)
                {
                    try
                    {
                        using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
                        {
                            isValid = context.ValidateCredentials(username, password);
                        }
                    }
                    catch (Exception e)
                    {
                        LoginDB.PrettifyAndLogException(ex, "getting login info from the domain");          // Domain exception
                        LoginDB.PrettifyAndLogException(e, "getting login info from the local machine");    // Machine exception
                    }
                }
            }


            return isValid;
        }
    }
}
