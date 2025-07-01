using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiExample.Models
{
    public class WeatherForecastDto
    {
        public int? Id { get; set; }
        public DateOnly? Date { get; set; }
        public int? TemperatureC { get; set; }
        public DateTime? CreatedAt { get; protected set; }
    }
}
