﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiExample.Features.WeatherForecast
{
    public class WeatherForecastQueryParameters
    {
        [JsonConverter(typeof(JsonStringEnumConverter<SortOptions>))]
        public enum SortOptions
        {
            Id,
            TemperatureC
        }

        // Filters
        public int? WhereTemperatureIsLargerThan { get; set; } = int.MinValue;

        // Sorting
        public SortOptions SortBy { get; set; } = SortOptions.Id;
        public bool Descending { get; set; } = false;

        // Paging
        public int Page { get; set; } = 0;

        [Range(1, 50)]
        public int PageSize { get; set; } = 50;
    }
}
