using AutoMapper;
using BusinessObject;
using DataAccess.Common;
using DataAccess.DAO;
using DataAccess.DTOs;
using DataAccess.Mapper;
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
        private readonly IMapper mapper;

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
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationMapper());
            });
            this.mapper = config.CreateMapper();
        }

        public void DeleteEmployee(string id)
        {
            EmployeeDAO.DeleteEmployee(id);
        }

        public async Task<EmployeeDTOResponse> GetEmployeeById(string id)
        {
            var e = mapper.Map<Employee, EmployeeDTOResponse>(EmployeeDAO.GetEmployeeById(id));
            var user = await userManager.FindByIdAsync(e.Id);
            var userRoles = await userManager.GetRolesAsync(user);
            e.RoleName = userRoles[0];
            return e;
        }

        public List<EmployeeDTOResponse> GetEmployees()
        {
            return mapper.Map<List<Employee>, List<EmployeeDTOResponse>>(EmployeeDAO.GetEmployees());
        }

        public async Task<EmployeeSignInResponse> SignInAsyn(EmployeeSignInModel userModel)
        {
            var user = await userManager.FindByEmailAsync(userModel.Email);
            var passwordValid = await userManager.CheckPasswordAsync(user, userModel.Password);
            if (user == null || !passwordValid)
            {
                return null;
            }



            var result = await signInManager.PasswordSignInAsync
                (userModel.Email, userModel.Password, false, false);


            if (!result.Succeeded)
            {
                return null;

            }

            var authClaims = new List<Claim>
            {

                new Claim(JwtRegisteredClaimNames.Email, userModel.Email),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, user.Id)
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
                    SecurityAlgorithms.HmacSha512)
            );

            string stringToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new EmployeeSignInResponse
            {
                Token = stringToken,
                UserId = user.Id,
                Role = userRoles[0].ToString()
            };
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



        public void UpdateEmployee(string id, EmployeePutDTORequest employee)
        {
            var e = mapper.Map<EmployeePutDTORequest, Employee>(employee);
            e.Id = id;
            EmployeeDAO.UpdateEmployee(e);
        }

        public async Task UpdateEmployeeRole(string id, string role)
        {
            var user = await userManager.FindByIdAsync(id);
            List<string> roles = new List<string>();
            roles.Add(AppRole.Employee);
            roles.Add(AppRole.Inventory);
            await userManager.RemoveFromRolesAsync(user, roles);
            await userManager.AddToRoleAsync(user, role);

        }
    }
}
