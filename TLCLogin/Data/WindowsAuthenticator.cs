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
