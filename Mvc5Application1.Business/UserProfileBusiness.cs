using Mvc5Application1.Business.Contracts.Security;
using Mvc5Application1.Data.Contracts;
using Mvc5Application1.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace Mvc5Application1.Business
{
    public class UserProfileBusiness : IUserProfileBusiness
    {
        private readonly IUserProfileRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileBusiness(IUserProfileRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public void AddUser(UserProfile user)
        {
            _userRepository.Add(user);
        }

        public void DeleteUser(UserProfile user)
        {
            _userRepository.Delete(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }

        public void UpdateUser(UserProfile user)
        {
            _userRepository.Update(user);
        }

        public List<UserProfile> GetUsers()
        {
            return _userRepository.GetAll().ToList();
        }

        public List<UserProfile> SearchUsers(SearchUserCriteria criteria)
        {
            return _userRepository.ExecuteSearch<UserProfile>("SearchUsers", criteria);
        }

        public void DeleteUserInProject(int projectId, int id)
        {
            var deleteSql = string.Format("EXEC DeleteUserInProject {0},{1}", projectId, id);
            _userRepository.ExecuteCommand(deleteSql);
            _unitOfWork.SaveChanges();
        }

        public void RemoveUser(int id)
        {
            var deleteSql = string.Format("EXEC DeleteUser {0}", id);
            _userRepository.ExecuteCommand(deleteSql);
            _unitOfWork.SaveChanges();
        }

        public bool CheckRemovable(int id)
        {
            var obj = _userRepository.GetById(id);
            return obj != null;
        }
    }
}