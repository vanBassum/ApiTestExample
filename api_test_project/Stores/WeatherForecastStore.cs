using ApiExample.Data;
using ApiExample.Data.Entities;
using ApiExample.Models;
using ApiExample.Queries;
using Microsoft.EntityFrameworkCore;


namespace ApiExample.Stores
{
    public class WeatherForecastStore : IStore<WeatherForecast, WeatherForecastQueryParameters>
    {
        private readonly AppDbContext _context;
        private readonly IMapper<WeatherForecast, WeatherForecastEntity> _mapper;
        private readonly IQueryBuilder<WeatherForecastQueryParameters, WeatherForecastEntity> _queryBuilder;

        public WeatherForecastStore(
            AppDbContext context,
            IMapper<WeatherForecast, WeatherForecastEntity> mapper,
            IQueryBuilder<WeatherForecastQueryParameters, WeatherForecastEntity> queryBuilder)
        {
            _context = context;
            _mapper = mapper;
            _queryBuilder = queryBuilder;
        }

        public async Task<List<WeatherForecast>> GetAllAsync(WeatherForecastQueryParameters parameters)
        {
            var query = _queryBuilder.Apply(_context.Forecasts.AsQueryable(), parameters);
            var entities = await query.ToListAsync();
            return entities.Select(_mapper.ToDto).ToList();
        }

        public async Task<WeatherForecast?> GetByIdAsync(int id)
        {
            var entity = await _context.Forecasts.FindAsync(id);
            return entity == null ? null : _mapper.ToDto(entity);
        }

        public async Task<WeatherForecast> CreateAsync(WeatherForecast dto)
        {
            var entity = _mapper.ToEntity(dto);
            _context.Forecasts.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.ToDto(entity);
        }

        public async Task<WeatherForecast> UpdateAsync(int id, WeatherForecast dto)
        {
            var entity = await _context.Forecasts.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"WeatherForecast with id {id} not found.");

            _mapper.UpdateEntity(dto, entity);
            await _context.SaveChangesAsync();

            return _mapper.ToDto(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Forecasts.FindAsync(id);
            if (entity != null)
            {
                _context.Forecasts.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
