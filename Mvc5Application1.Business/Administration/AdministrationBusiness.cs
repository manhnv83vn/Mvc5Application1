using Mvc5Application1.Business.Contracts.Administration;
using Mvc5Application1.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Mvc5Application1.Business.Administration
{
    public class AdministrationBusiness : IAdministrationBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserProfile> _userProfileRepository;
        private readonly IRepository<Employees> _employeeRepository;
        private readonly IRepository<ProjectRole> _projectRoleRepository;

        public AdministrationBusiness(IUnitOfWork unitOfWork,
            IRepository<UserProfile> userProfileRepository,
            IRepository<Employees> employeeRepository,
            IRepository<ProjectRole> projectRoleRepository)
        {
            _projectRoleRepository = projectRoleRepository;
            _userProfileRepository = userProfileRepository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public UserProfile GetUserProfileById(int userId)
        {
            return _userProfileRepository.GetById(userId);
        }

        public List<ProjectRole> ListProjectRole()
        {
            return _projectRoleRepository.GetAll().ToList();
        }

        public List<UserProfile> ListUserProfile()
        {
            return _userProfileRepository.GetAll().ToList();
        }

        public UserProfile GetUserProfileDetailsByUserName(string userName)
        {
            return _userProfileRepository.GetAll().FirstOrDefault(t => t.UserName.Equals(userName));
        }

        public UserProfile GetUserProfile()
        {
            var principal = Thread.CurrentPrincipal;
            if (principal == null)
                throw new ApplicationException("Your session has timeout. Please login again.");
            return _userProfileRepository.GetAll().FirstOrDefault(x => x.UserName == principal.Identity.Name);
        }

        public List<Employees> GetEmployeeses()
        {
            return _employeeRepository.GetAll().ToList();
        }

        public void AddRangeEmployeeNoSaveChange(List<Employees> employeeses)
        {
            foreach (var item in employeeses)
            {
                _employeeRepository.Add(item);
            }
        }

        public void UpdateRangeEmployeeNoSaveChange(List<Employees> employeeses)
        {
            foreach (var item in employeeses)
            {
                _employeeRepository.Update(item);
            }
        }

        public void EmployeesSaveChange()
        {
            _unitOfWork.SaveChanges();
        }

        public List<vwEmployee> SearchEmployees(SearchEmployeeCriteria criteria)
        {
            var result = _employeeRepository.ExecuteSearch<vwEmployee>("SearchEmployeeList", criteria);
            return result;
        }

        public void DeleteEmployee(int id)
        {
            _employeeRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }

        public Employees GetEmployeeDetailsById(int id)
        {
            var data = _employeeRepository.GetAll().FirstOrDefault(t => t.EmployeeId == id);
            return data;
        }
    }
}