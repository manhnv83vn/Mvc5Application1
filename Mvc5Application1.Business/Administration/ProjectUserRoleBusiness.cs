using Mvc5Application1.Business.Contracts.Administration;
using Mvc5Application1.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace Mvc5Application1.Business.Administration
{
    public class ProjectUserRoleBusiness : IProjectUserRoleBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserProfile> _userProfileRepository;
        private readonly IRepository<Permissions> _permissionRepository;
        private readonly IRepository<ProjectPermissions> _projectPermissionsRepository;
        private readonly IRepository<ProjectRole> _projectRoleRepository;
        private readonly IRepository<vwUserRoles> _userRolesRepository;

        public ProjectUserRoleBusiness(IUnitOfWork unitOfWork, IRepository<UserProfile> userProfileRepository,
            IRepository<Permissions> permissionRepository, IRepository<ProjectPermissions> projectPermissionsRepository,
            IRepository<ProjectRole> projectRoleRepository, IRepository<vwUserRoles> userRolesRepository)
        {
            _userProfileRepository = userProfileRepository;
            _projectPermissionsRepository = projectPermissionsRepository;
            _permissionRepository = permissionRepository;
            _projectRoleRepository = projectRoleRepository;
            _userRolesRepository = userRolesRepository;
            _unitOfWork = unitOfWork;
        }

        //Add UserProfile
        public void AddUserProfile(UserProfile user)
        {
            _userProfileRepository.Add(user);
            _unitOfWork.SaveChanges();
        }

        public List<UserProfile> GetListUserProfiles()
        {
            return _userProfileRepository.GetAll().ToList();
        }

        public void AddUserPermission(int userId, int projectRoleId)
        {
            var permissions = GetPermissionBaseProjectRole(projectRoleId);
            foreach (var permission in permissions)
            {
                var projectPermission = new ProjectPermissions
                {
                    UserId = userId,
                    PermissionId = permission.PermissionId
                };

                _projectPermissionsRepository.Add(projectPermission);
                _unitOfWork.SaveChanges();
            }
        }

        private List<Permissions> GetPermissionBaseProjectRole(int projectRoleId)
        {
            return _permissionRepository.GetAll().Where(x => x.ProjectRoleId == projectRoleId).ToList();
        }

        public void DeleteProjectUserRole(int userId)
        {
            var deleteSql = string.Format("DELETE FROM dbo.ProjectPermissions WHERE UserId = {0} ", userId);
            _projectRoleRepository.ExecuteCommand(deleteSql);
            _unitOfWork.SaveChanges();
        }

        public List<int> GetProjectRoleIdByUser(int userId)
        {
            var criteria = new SearchUserCriteria
            {
                UserId = userId
            };
            return _projectRoleRepository.ExecuteSearch<int>("SearchProjectRoleByUserName", criteria);
        }

        public List<vwUserRoles> GetListUserRoles()
        {
            return _userRolesRepository.GetAll().ToList();
        }
    }
}