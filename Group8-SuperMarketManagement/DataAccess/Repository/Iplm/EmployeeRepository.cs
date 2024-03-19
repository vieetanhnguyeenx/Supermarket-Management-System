using BusinessObject;
using DataAccess.Common;
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

        public async Task<IdentityResult> SigUpAsyn(EmployeeSignUpModel user)
        {
            var userSignUp = new Employee
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.Email,
                Address = user.Address,
                DoB = user.DoB,
                Discontinued = false,
                HireDate = user.HireDate,
                Phone = user.Phone,
                PhoneNumber = user.Phone,

            };
            var result = await userManager.CreateAsync(userSignUp, user.Password);

            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync(AppRole.Employee))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRole.Employee));
                }

                await userManager.AddToRoleAsync(userSignUp, AppRole.Employee);
            }
            return result;
        }
    }
}
