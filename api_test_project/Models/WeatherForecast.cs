using System.ComponentModel.DataAnnotations;

namespace api_test_project.Models
{
    public class WeatherForecast
    {
        [Key]
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
    }
}
