using ApiExample.Data.Entities;
using ApiExample.Models;

namespace ApiExample.Queries
{
    public class WeatherForecastQueryBuilder : IQueryBuilder<WeatherForecastQueryParameters, WeatherForecastEntity>
    {
        public IQueryable<WeatherForecastEntity> Apply(IQueryable<WeatherForecastEntity> query, WeatherForecastQueryParameters parameters)
        {
            query = ApplyFiltering(query, parameters);
            query = ApplySorting(query, parameters);
            query = ApplyPaging(query, parameters);
            return query;
        }

        private IQueryable<WeatherForecastEntity> ApplyFiltering(IQueryable<WeatherForecastEntity> query, WeatherForecastQueryParameters parameters)
        {
            // Placeholder for filters
            return query;
        }

        private IQueryable<WeatherForecastEntity> ApplySorting(IQueryable<WeatherForecastEntity> query, WeatherForecastQueryParameters parameters)
        {
            return (parameters.SortBy, parameters.Descending) switch
            {
                (WeatherForecastQueryParameters.SortOptions.Id, false) => query.OrderBy(x => x.Id),
                (WeatherForecastQueryParameters.SortOptions.Id, true) => query.OrderByDescending(x => x.Id),
                (WeatherForecastQueryParameters.SortOptions.TemperatureC, false) => query.OrderBy(x => x.TemperatureC),
                (WeatherForecastQueryParameters.SortOptions.TemperatureC, true) => query.OrderByDescending(x => x.TemperatureC),
                _ => query
            };
        }

        private IQueryable<WeatherForecastEntity> ApplyPaging(IQueryable<WeatherForecastEntity> query, WeatherForecastQueryParameters parameters)
        {
            return query
                .Skip(parameters.Page * parameters.PageSize)
                .Take(parameters.PageSize);
        }
    }




}
