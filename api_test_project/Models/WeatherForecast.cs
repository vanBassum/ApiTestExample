using System.ComponentModel.DataAnnotations;

namespace ApiExample.Models
{
    public class WeatherForecast
    {
        public int? Id { get; set; }
        public DateOnly? Date { get; set; }
        public int? TemperatureC { get; set; }
    }
}
