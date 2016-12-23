using System;
using System.Threading;

namespace Mvc5Application1.Framework.Security
{
    public static class Mvc5Application1PrincipalHelpers
    {
        public static string GetUserName()
        {
            var principal = Thread.CurrentPrincipal; // as Mvc5Application1Principal;
            if (principal == null)
            {
                throw new ApplicationException("Your session has timeout. Please login again.");
            }
            return principal.Identity.Name;
        }
    }
}