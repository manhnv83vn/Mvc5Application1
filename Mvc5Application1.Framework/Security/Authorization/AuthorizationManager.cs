using Mvc5Application1.Business.Contracts.Security;
using System;

namespace Mvc5Application1.Framework.Security.Authorization
{
    public class AuthorizationManager
    {
        public IAuthorizationBusiness AuthorizationBusiness { get; set; }

        public AuthorizationManager(IAuthorizationBusiness authorizationBusiness)
        {
            AuthorizationBusiness = authorizationBusiness;
        }

        public bool CheckAccess(string resource)
        {
            if (string.IsNullOrEmpty(resource))
            {
                throw new ArgumentNullException("resource");
            }

            var permission = AuthorizationBusiness.CheckPermission(resource);
            return permission;
        }
    }
}