using Mvc5Application1.Data.Model;
using System.Collections.Generic;

namespace Mvc5Application1.Business.Contracts.Security
{
    public interface IUserProfileBusiness
    {
        void AddUser(UserProfile user);

        void DeleteUser(UserProfile user);

        void UpdateUser(UserProfile user);

        List<UserProfile> GetUsers();

        List<UserProfile> SearchUsers(SearchUserCriteria criteria);

        void DeleteUserInProject(int projectId, int id);

        void RemoveUser(int id);

        bool CheckRemovable(int id);
    }
}