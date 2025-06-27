using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using api_test_project.Data;

namespace TestProject
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Check environment
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env == "Development")
                {
                    db.Database.EnsureCreated(); // For InMemory
                }
                else
                {
                    db.Database.Migrate(); // ✅ Apply real migrations for MySQL
                }
            });
        }
    }
}
