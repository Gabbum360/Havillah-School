using ApplicationServices.Abstractions;
using ApplicationServices.AuthenticationManagement;
using ApplicationServices.Common.Options;
using ApplicationServices.DataStorage;
using ApplicationServices.Entities;
using ApplicationServices.Features.Student;
using ApplicationServices.Interfaces;
using ApplicationServices.Interfaces.StudentInterfaces;
using ApplicationServices.Services.Mail;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Havillah_SchoolManagement_System
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //install this using package manager console...
            //dotnet tool install --global dotnet-ef
            //then Add-Migration

            //MySql Connection.
            var connectionString = configuration.GetConnectionString("SMConnection");
            services.AddDbContextPool<SMDatabaseContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            services.AddIdentityCore<ApplicationUser>(option =>
            {
                option.Tokens.EmailConfirmationTokenProvider = "emailConfirmation";
            })//.AddRoles<ApplicationRole>().AddEntityFrameworkStores<IdpDbContext>()
            .AddEntityFrameworkStores<SMDatabaseContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 8;
            });
            /*Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });*/
            services.Configure<MailOption>(configuration.GetSection("MailOptions"));
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IMailManager, MailManager>();
            services.AddHttpClient<IMailService, MailService>();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly((Assembly.GetExecutingAssembly()));
                //cfg.RegisterServicesFromAssembly(typeof(LoginCommand));
            });
            services.AddTransient<IStudent, StudentLogic>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddApiVersioning(cfg =>
            {
                cfg.AssumeDefaultVersionWhenUnspecified = true;
            });
            return services;
        }
    }
}
