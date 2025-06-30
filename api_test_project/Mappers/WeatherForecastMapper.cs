using ApiExample.Data.Entities;

namespace ApiExample.Models
{
    public class WeatherForecastMapper : IMapper<WeatherForecast, WeatherForecastEntity>
    {
        public WeatherForecast ToDto(WeatherForecastEntity entity)
        {
            var dto = new WeatherForecast();
            UpdateDto(entity, dto);
            return dto;
        }

        public WeatherForecastEntity ToEntity(WeatherForecast dto)
        {
            var entity = new WeatherForecastEntity();
            UpdateEntity(dto, entity);
            return entity;
        }

        public void UpdateDto(WeatherForecastEntity entity, WeatherForecast dto)
        {
            dto.Id = entity.Id;
            dto.Date = entity.Date;
            dto.TemperatureC = entity.TemperatureC;
        }

        public void UpdateEntity(WeatherForecast dto, WeatherForecastEntity entity)
        {
            entity.Date = dto.Date ?? entity.Date;
            entity.TemperatureC = dto.TemperatureC ?? entity.TemperatureC;
        }
    }
}
