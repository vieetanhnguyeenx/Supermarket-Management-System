using BusinessObject;
using DataAccess.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repository.Iplm
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly UserManager<Employee> userManager;
        private readonly SignInManager<Employee> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        public EmployeeRepository(
            UserManager<Employee> userManager,
            SignInManager<Employee> signInManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }
        public Task<string> SignInAsyn(EmployeeSignInModel user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> SigUpAsyn(EmployeeSignUpModel user)
        {
            throw new NotImplementedException();
        }
    }
}
