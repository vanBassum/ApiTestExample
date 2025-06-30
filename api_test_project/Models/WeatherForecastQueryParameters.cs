using ApiExample.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiExample.Models
{
    public interface IQueryBuilder<TEntity>
    {
        Func<IQueryable<TEntity>, IQueryable<TEntity>> BuildQuery();
    }

    public class WeatherForecastQueryParameters : IQueryBuilder<WeatherForecastEntity>
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

        public Func<IQueryable<WeatherForecastEntity>, IQueryable<WeatherForecastEntity>> BuildQuery()
        {
            return query =>
            {
                query = ApplySorting(query);
                query = ApplyPaging(query);
                return query;
            };
        }

        private IQueryable<WeatherForecastEntity> ApplySorting(IQueryable<WeatherForecastEntity> query)
        {
            return (SortBy, Descending) switch
            {
                (SortOptions.Id, false) => query.OrderBy(x => x.Id),
                (SortOptions.Id, true) => query.OrderByDescending(x => x.Id),
                (SortOptions.TemperatureC, false) => query.OrderBy(x => x.TemperatureC),
                (SortOptions.TemperatureC, true) => query.OrderByDescending(x => x.TemperatureC),
                _ => query
            };
        }

        private IQueryable<WeatherForecastEntity> ApplyPaging(IQueryable<WeatherForecastEntity> query)
        {
            return query.Skip(Page * PageSize).Take(PageSize);
        }
    }
}
