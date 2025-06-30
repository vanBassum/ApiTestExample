using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ApiExample.Data;

namespace Testing.TestUtils
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
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var provider = db.Database.ProviderName ?? throw new NullReferenceException("Db provider name is null");

                if (provider.Contains("InMemory"))
                {
                    db.Database.EnsureCreated(); // Dev / InMemory
                }
                else
                {
                    db.Database.Migrate(); // CI / MySQL
                }
            });
        }
    }
}
