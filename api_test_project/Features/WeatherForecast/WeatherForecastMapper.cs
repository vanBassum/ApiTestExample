using ApiExample.Infrastructure.Mapping;

namespace ApiExample.Features.WeatherForecast
{
    public class WeatherForecastMapper : IMapper<WeatherForecastDto, WeatherForecastEntity>
    {
        public WeatherForecastDto ToDto(WeatherForecastEntity entity)
        {
            var dto = new WeatherForecastDto();
            UpdateDto(entity, dto);
            return dto;
        }

        public WeatherForecastEntity ToEntity(WeatherForecastDto dto)
        {
            var entity = new WeatherForecastEntity();
            UpdateEntity(dto, entity);
            return entity;
        }

        public void UpdateDto(WeatherForecastEntity entity, WeatherForecastDto dto)
        {
            dto.Id = entity.Id;
            dto.Date = entity.Date;
            dto.TemperatureC = entity.TemperatureC;
        }

        public void UpdateEntity(WeatherForecastDto dto, WeatherForecastEntity entity)
        {
            entity.Date = dto.Date ?? entity.Date;
            entity.TemperatureC = dto.TemperatureC ?? entity.TemperatureC;
        }
    }
}
