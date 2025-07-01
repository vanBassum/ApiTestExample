using ApiExample.Data.Entities;

namespace ApiExample.Data.Seeders
{
    public class WeatherForecastSeeder : ISeeder
    {
        public void Seed(AppDbContext context)
        {
            if (context.Forecasts.Any()) return;

            var rng = new Random();
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild",
                "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var forecasts = Enumerable.Range(1, 10).Select(i => new WeatherForecastEntity
            {
                Date = DateOnly.FromDateTime(DateTime.Today.AddDays(i)),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            }).ToList();

            context.Forecasts.AddRange(forecasts);
        }
    }
}
