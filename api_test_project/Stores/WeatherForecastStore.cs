using ApiExample.Data;
using ApiExample.Data.Entities;
using ApiExample.Models;
using ApiExample.Queries;
using Microsoft.EntityFrameworkCore;


namespace ApiExample.Stores
{
    public class WeatherForecastStore : IStore<WeatherForecast, WeatherForecastEntity>
    {
        private readonly AppDbContext _context;

        public WeatherForecastStore(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<WeatherForecast>> GetAllAsync(IQueryOptions<WeatherForecastEntity> queryOptions)
        {
            var query = queryOptions.Apply(_context.Forecasts.AsQueryable());
            var entities = await query.ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<WeatherForecast?> GetByIdAsync(int id)
        {
            var entity = await _context.Forecasts.FindAsync(id);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<WeatherForecast> CreateAsync(WeatherForecast dto)
        {
            var entity = MapToEntity(dto);
            _context.Forecasts.Add(entity);
            await _context.SaveChangesAsync();

            return MapToDto(entity);
        }

        public async Task<WeatherForecast> UpdateAsync(int id, WeatherForecast dto)
        {
            var entity = await _context.Forecasts.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"WeatherForecast with id {id} not found.");

            UpdateEntity(dto, entity);
            await _context.SaveChangesAsync();

            return MapToDto(entity);
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



        // These 4 functions could end up in a mapper class.
        private static WeatherForecast MapToDto(WeatherForecastEntity entity)
        {
            var dto = new WeatherForecast();
            UpdateDto(entity, dto);
            return dto;
        }

        private static WeatherForecastEntity MapToEntity(WeatherForecast dto)
        {
            var entity = new WeatherForecastEntity();
            UpdateEntity(dto, entity);
            return entity;
        }

        private static void UpdateDto(WeatherForecastEntity entity, WeatherForecast dto)
        {
            dto.Id = entity.Id;
            dto.Date = entity.Date;
            dto.TemperatureC = entity.TemperatureC;
        }

        private static void UpdateEntity(WeatherForecast dto, WeatherForecastEntity entity)
        {
            entity.Date = dto.Date ?? entity.Date;
            entity.TemperatureC = dto.TemperatureC ?? entity.TemperatureC;
        }
    }







}
