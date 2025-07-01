using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiExample.Features.WeatherForecast
{
    public class WeatherForecastDto
    {
        public int? Id { get; set; }
        public DateOnly? Date { get; set; }
        public int? TemperatureC { get; set; }
        public string Summary { get; set; } = string.Empty;
    }
}
