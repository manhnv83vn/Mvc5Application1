using Mvc5Application1.Framework.Security.Authorization;

namespace Mvc5Application1.Helpers
{
    public static class PermissionHelper
    {
        public static bool CheckAccess(string function)
        {
            return Mvc5Application1Permission.CheckAccess(function);
        }
    }
}