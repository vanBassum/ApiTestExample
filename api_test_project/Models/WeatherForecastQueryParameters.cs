using ApiExample.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiExample.Models
{
    public class WeatherForecastQueryParameters
    {
        [JsonConverter(typeof(JsonStringEnumConverter<SortOptions>))]
        public enum SortOptions
        {
            Id,
            TemperatureC
        }

        public SortOptions SortBy { get; set; } = SortOptions.Id;
        public bool Descending { get; set; } = false;
        public int Page { get; set; } = 0;

        [Range(1, 50)]
        public int PageSize { get; set; } = 50;
    }
}
