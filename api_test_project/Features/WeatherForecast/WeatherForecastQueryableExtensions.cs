using System.Linq.Expressions;

namespace ApiExample.Features.WeatherForecast
{
    public static class WeatherForecastQueryableExtensions
    {
        public static IQueryable<WeatherForecastEntity> FilterBy(this IQueryable<WeatherForecastEntity> query, WeatherForecastQueryParameters parameters)
        {
            return query.Where(i => i.TemperatureC > parameters.WhereTemperatureIsLargerThan);
        }

        public static IQueryable<WeatherForecastEntity> SortBy(this IQueryable<WeatherForecastEntity> query, WeatherForecastQueryParameters parameters)
        {
            Expression<Func<WeatherForecastEntity, object>> keySelector = parameters.SortBy switch
            {
                WeatherForecastQueryParameters.SortOptions.Id => x => x.Id,
                WeatherForecastQueryParameters.SortOptions.TemperatureC => x => x.TemperatureC,
                _ => x => x.Id // default
            };

            query = parameters.Descending
                ? query.OrderByDescending(keySelector)
                : query.OrderBy(keySelector);

            return query;
        }

        public static IQueryable<WeatherForecastEntity> Paginate(this IQueryable<WeatherForecastEntity> query, WeatherForecastQueryParameters parameters)
        {
            return query.Skip(parameters.Page * parameters.PageSize).Take(parameters.PageSize);
        }
    }
}
