using ApiExample.Data.Entities;

namespace ApiExample.Queries
{
    public class WeatherForecastQuery : IQueryOptions<WeatherForecastEntity>
    {
        public WeatherForecastSortOption SortBy { get; set; } = WeatherForecastSortOption.Id;
        public bool Descending { get; set; } = false;

        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 50;

        public IQueryable<WeatherForecastEntity> Apply(IQueryable<WeatherForecastEntity> query)
        {
            query = ApplyFiltering(query);
            query = ApplySorting(query);
            query = ApplyPaging(query);
            return query;
        }

        private IQueryable<WeatherForecastEntity> ApplyFiltering(IQueryable<WeatherForecastEntity> query)
        {
            return query;
        }

        private IQueryable<WeatherForecastEntity> ApplySorting(IQueryable<WeatherForecastEntity> query)
        {
            return (SortBy, Descending) switch
            {
                (WeatherForecastSortOption.Id, false) => query.OrderBy(d => d.Id),
                (WeatherForecastSortOption.Id, true) => query.OrderByDescending(d => d.Id),
                (WeatherForecastSortOption.TemperatureC, false) => query.OrderBy(d => d.TemperatureC),
                (WeatherForecastSortOption.TemperatureC, true) => query.OrderByDescending(d => d.TemperatureC),
                _ => query
            };
        }

        private IQueryable<WeatherForecastEntity> ApplyPaging(IQueryable<WeatherForecastEntity> query)
        {
            return query.Skip(Page * PageSize).Take(PageSize);
        }
    }




}
