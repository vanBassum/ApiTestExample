using System.ComponentModel.DataAnnotations;

namespace ApiExample.Features.WeatherForecast
{
    public class WeatherForecastEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
    }
}
