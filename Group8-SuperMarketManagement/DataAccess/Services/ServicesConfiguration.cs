
using BusinessObject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DataAccess.Services
{
    public static class ServicesConfiguration
    {

        public static void AddCustomServices(this IServiceCollection services)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            services.AddDbContext<MyDBContext>(options =>
            {
                options.UseSqlServer("DefaultConnection");
            });


            services.AddIdentity<Employee, IdentityRole>()
                    .AddEntityFrameworkStores<MyDBContext>().AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = configuration["https://localhost:5000"],
                    ValidIssuer = configuration["https://localhost:5000"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ThisIsSecretKey1234567890DoiDenQuaThisIsSecretKey1234567890DoiDenQuaThisIsSecretKey1234567890DoiDenQuaThisIsSecretKey1234567890DoiDenQuaThisIsSecretKey1234567890DoiDenQua"]))

                };
            });

        }

    }
}
