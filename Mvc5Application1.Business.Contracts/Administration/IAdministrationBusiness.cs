using Mvc5Application1.Data.Model;
using System.Collections.Generic;

namespace Mvc5Application1.Business.Contracts.Administration
{
    public interface IAdministrationBusiness
    {
        UserProfile GetUserProfileById(int userId);

        List<ProjectRole> ListProjectRole();

        UserProfile GetUserProfileDetailsByUserName(string userName);

        UserProfile GetUserProfile();

        List<Employees> GetEmployeeses();

        void AddRangeEmployeeNoSaveChange(List<Employees> employeeses);

        void UpdateRangeEmployeeNoSaveChange(List<Employees> employeeses);

        void EmployeesSaveChange();

        List<vwEmployee> SearchEmployees(SearchEmployeeCriteria criteria);

        void DeleteEmployee(int id);

        Employees GetEmployeeDetailsById(int id);
    }
}