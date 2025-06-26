using api_test_project.Models;
using Microsoft.EntityFrameworkCore;

namespace api_test_project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherForecast> Forecasts => Set<WeatherForecast>();
    }
}
