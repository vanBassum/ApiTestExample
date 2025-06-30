using System.ComponentModel.DataAnnotations;

namespace ApiExample.Data.Entities
{
    public class WeatherForecastEntity
    {
        [Key]
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
    }
}
