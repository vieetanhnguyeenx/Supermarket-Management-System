using DataAccess.DTOs;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Repository
{
    public interface IEmployeeRepository
    {
        Task<IdentityResult> SigUpAsyn(EmployeeSignUpModel user);
        Task<EmployeeSignInResponse> SignInAsyn(EmployeeSignInModel user);
        List<EmployeeDTOResponse> GetEmployees();
        Task<EmployeeDTOResponse> GetEmployeeById(string id);
        void UpdateEmployee(string id, EmployeePutDTORequest employee);
        void DeleteEmployee(string id);
        Task UpdateEmployeeRole(string id, string role);
    }
}
