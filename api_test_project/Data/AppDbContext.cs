using ApiExample.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherForecastEntity> Forecasts => Set<WeatherForecastEntity>();
    }
}
