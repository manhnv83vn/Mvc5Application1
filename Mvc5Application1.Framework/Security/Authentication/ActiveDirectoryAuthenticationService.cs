using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace Mvc5Application1.Framework.Security.Authentication
{
    public class ActiveDirectoryAuthenticationService : IAuthenticationService
    {
        public string LdapServer { get; set; }
        public string Scope { get; set; }

        public ActiveDirectoryAuthenticationService()
        {
            LdapServer = ConfigurationManager.AppSettings["LdapServer"];
            Scope = ConfigurationManager.AppSettings["Scope"];
        }

        public bool Authenticate(string userName, string password)
        {
            try
            {
                using (var pc = new PrincipalContext(ContextType.Domain, LdapServer, Scope))
                {
                    bool isValid = pc.ValidateCredentials(userName, password);
                    return isValid;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string[] GetGroups(string userName)
        {
            var result = new List<string>();
            using (var yourDomain = new PrincipalContext(ContextType.Domain, LdapServer, Scope))
            {
                using (var user = UserPrincipal.FindByIdentity(yourDomain, userName))
                {
                    if (user != null) result.AddRange(user.GetGroups().Select(@group => @group.Name));
                }
            }
            return result.ToArray();
        }
    }
}