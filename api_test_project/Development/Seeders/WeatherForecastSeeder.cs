using ApiExample.Data;
using ApiExample.Features.WeatherForecast;
using ApiExample.Infrastructure.Seeding;

namespace ApiExample.Development.Seeders
{
    public class WeatherForecastSeeder : ISeeder
    {
        public void Seed(AppDbContext context)
        {
            if (context.Forecasts.Any()) return;

            var rng = new Random();

            var forecasts = Enumerable.Range(1, 10).Select(i => new WeatherForecastEntity
            {
                Date = DateOnly.FromDateTime(DateTime.Today.AddDays(i)),
                TemperatureC = rng.Next(-20, 55)
            }).ToList();

            context.Forecasts.AddRange(forecasts);
        }
    }
}
