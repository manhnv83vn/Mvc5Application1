using Mvc5Application1.Data.Model;
using System.Collections.Generic;

namespace Mvc5Application1.Business.Contracts.Administration
{
    public interface IProjectUserRoleBusiness
    {
        void AddUserProfile(UserProfile user);

        void AddUserPermission(int userId, int projectRoleId);

        List<UserProfile> GetListUserProfiles();

        List<vwUserRoles> GetListUserRoles();

        void DeleteProjectUserRole(int userId);

        List<int> GetProjectRoleIdByUser(int userId);
    }
}