using Mvc5Application1.Business.Contracts;
using Mvc5Application1.Business.Contracts.Security;
using Mvc5Application1.Data.Model;
using Mvc5Application1.Framework.Caching;
using System.Linq;
using System.Threading;

namespace Mvc5Application1.Business.Security
{
    /// <summary>
    /// Class AuthorizationBusiness
    /// </summary>
    public class AuthorizationBusiness : IAuthorizationBusiness
    {
        private readonly IRepository<UserProfile> _userProfileRepository;
        private readonly IRepository<SpecicalPermission> _specialPermissionRepository;
        private readonly ISecurityMatrixRepository _securityMatrixRepository;
        private readonly ICacheManager _cacheManager;

        public AuthorizationBusiness(ISecurityMatrixRepository securityMatrixRepository, ICacheManager cacheManager, IRepository<UserProfile> userProfileRepository, IRepository<SpecicalPermission> specialPermissionRepository)
        {
            _securityMatrixRepository = securityMatrixRepository;
            _cacheManager = cacheManager;
            _userProfileRepository = userProfileRepository;
            _specialPermissionRepository = specialPermissionRepository;
        }

        public bool CheckPermission(string function)
        {
            var user = _userProfileRepository.GetAll().FirstOrDefault(x => x.UserName == Thread.CurrentPrincipal.Identity.Name);

            if (user != null && (user.IsGroupAdmin == true && function == "Add User roles"))
                return true;

            if (function == "EditUser" && user != null)
            {
                var hasPermission = _specialPermissionRepository.GetAll().FirstOrDefault(x => x.UserId == user.UserId);

                return hasPermission != null;
            }

            var securityMappingList = _securityMatrixRepository.GetSecurityMatrix();

            var securityMatrixDict = new SecurityMatrixDict(securityMappingList);

            _cacheManager.Clear();

            _cacheManager.Set("SecurityMatrixDict", securityMatrixDict, 120);

            return securityMatrixDict.GetValue(function) && user != null && user.IsGroupAdmin == true;
        }
    }
}