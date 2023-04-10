using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managemrnt.EFCore
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var hostPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Management.Host");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(hostPath)
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.Development.json", true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(
                configuration.GetConnectionString("DefaultConnection"), 
                new MySqlServerVersion(configuration["MySql:Version"]));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
