using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.DataStorage
{
    public class SMDatabaseFactory : IDesignTimeDbContextFactory<SMDatabaseContext>
    {
        public SMDatabaseContext CreateDbContext(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("secrets.json", optional: true)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("SMDbConnection");
            var optionsBuilder = new DbContextOptionsBuilder<SMDatabaseContext>();
            optionsBuilder.UseMySql<SMDatabaseContext>(ServerVersion.AutoDetect(connectionString));
            return new SMDatabaseContext(optionsBuilder.Options);
        }
    }
}
