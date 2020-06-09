using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AuthServer.Infrastructure
{
    public class ApplicationIdentityDbContextFactory : IDesignTimeDbContextFactory<ApplicationIdentityDbContext>
    {
        public ApplicationIdentityDbContext CreateDbContext(string[] args)
        {
            var dbContext = new ApplicationIdentityDbContext(
                new DbContextOptionsBuilder<ApplicationIdentityDbContext>()
                    .UseSqlServer(
                        new ConfigurationBuilder()
                            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.json"))
                            .Build()
                            .GetConnectionString("DefaultConnection")
                    ).Options);

            dbContext.Database.Migrate();
            return dbContext;
        }
    }
}