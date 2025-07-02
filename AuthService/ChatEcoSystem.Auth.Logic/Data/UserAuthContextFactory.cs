using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ChatEcoSystem.Auth.Logic.Data
{
    public class UserAuthContextFactory : IDesignTimeDbContextFactory<UserAuthContext>
    {
        public UserAuthContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<UserAuthContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new UserAuthContext(optionsBuilder.Options);
        }
    }
} 