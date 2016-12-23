using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;

namespace Mvc5Application1.Framework.Security.Authentication
{
    public class LdapAuthentication : IAuthenticationService
    {
        private readonly string _domain;
        private readonly string _ldapGroups;
        private string _filterAttribute;
        private string _ldapPath;

        public LdapAuthentication()
        {
            _ldapPath = ConfigurationManager.AppSettings["LdapPath"];
            _domain = ConfigurationManager.AppSettings["Domain"];
            _ldapGroups = ConfigurationManager.AppSettings["LdapGroups"];
        }

        public bool Authenticate(string username, string pwd)
        {
            var domainAndUsername = username;
            if (!string.IsNullOrEmpty(_domain) && !username.Contains(@"\"))
            {
                domainAndUsername = _domain + @"\" + username;
            }

            if (username.Contains(@"\"))
            {
                var idx = username.IndexOf(@"\", StringComparison.Ordinal);
                username = username.Substring(idx + 1);
            }

            var entry = new DirectoryEntry(_ldapPath, domainAndUsername, pwd, AuthenticationTypes.Secure);

            try
            {
                var searcher = new DirectorySearcher(entry) { Filter = "(SAMAccountName=" + username + ")" };
                searcher.PropertiesToLoad.Add("cn");
                var result = searcher.FindOne();

                if (null == result)
                {
                    return false;
                }

                var userGroups = GetGroups(searcher);
                var inLdapGroups = _ldapGroups.Split(',').Any(g => userGroups.Contains(g));

                if (!inLdapGroups)
                {
                    return false;
                }

                _ldapPath = result.Path;
                _filterAttribute = (String)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public string[] GetGroups(string userName)
        {
            var search = new DirectorySearcher(_ldapPath) { Filter = "(cn=" + _filterAttribute + ")" };
            search.PropertiesToLoad.Add("memberOf");
            var groupNames = new List<string>();
            try
            {
                var result = search.FindOne();
                var propertyCount = result.Properties["memberOf"].Count;

                for (var propertyCounter = 0;
                    propertyCounter < propertyCount;
                    propertyCounter++)
                {
                    var dn = (string)result.Properties["memberOf"][propertyCounter];

                    var equalsIndex = dn.IndexOf("=", 1, StringComparison.Ordinal);
                    var commaIndex = dn.IndexOf(",", 1, StringComparison.Ordinal);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    groupNames.Add(dn.Substring((equalsIndex + 1),
                        (commaIndex - equalsIndex) - 1));
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
            return groupNames.ToArray();
        }

        private List<string> GetGroups(DirectorySearcher searcher)
        {
            searcher.PropertiesToLoad.Add("memberOf");
            var groupNames = new List<string>();

            try
            {
                var result = searcher.FindOne();
                var propertyCount = result.Properties["memberOf"].Count;

                for (var propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    var dn = (string)result.Properties["memberOf"][propertyCounter];
                    var equalsIndex = dn.IndexOf("=", 1, StringComparison.Ordinal);
                    var commaIndex = dn.IndexOf(",", 1, StringComparison.Ordinal);

                    if (-1 == equalsIndex)
                    {
                        return null;
                    }

                    groupNames.Add(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return groupNames;
        }
    }
}