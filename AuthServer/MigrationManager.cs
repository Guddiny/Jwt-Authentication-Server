using System;
using AuthServer.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthServer
{
    public static class MigrationManager
    {
        public static IWebHost MigrateDatabase(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
                try
                {
                    context.Database.EnsureCreated();
                    context.Database.Migrate();
                }
                catch (Exception)
                {
                    //Log errors or do anything you think it's needed
                    throw;
                }
            }

            return webHost;
        }
    }
}