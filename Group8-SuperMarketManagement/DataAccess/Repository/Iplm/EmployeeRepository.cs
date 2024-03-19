using BusinessObject;
using DataAccess.Common;
using DataAccess.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public async Task<string> SignInAsyn(EmployeeSignInModel userModel)
        {
            var user = await userManager.FindByEmailAsync(userModel.Email);
            var passwordValid = await userManager.CheckPasswordAsync(user, userModel.Password);
            if (user == null || !passwordValid)
            {
                return string.Empty;
            }



            var result = await signInManager.PasswordSignInAsync
                (userModel.Email, userModel.Password, false, false);


            if (!result.Succeeded)
            {
                return string.Empty;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userModel.Email),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    authenKey,
                    SecurityAlgorithms.HmacSha512Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
