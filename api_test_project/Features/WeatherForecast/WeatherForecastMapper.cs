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
            dto.Summary = GetSummary(entity.TemperatureC);
        }

        public void UpdateEntity(WeatherForecastDto dto, WeatherForecastEntity entity)
        {
            entity.Date = dto.Date ?? entity.Date;
            entity.TemperatureC = dto.TemperatureC ?? entity.TemperatureC;
        }
        private string GetSummary(int temperatureC)
        {
            return temperatureC switch
            {
                <= 0 => "Freezing",
                <= 10 => "Cold",
                <= 20 => "Cool",
                <= 25 => "Mild",
                <= 30 => "Warm",
                <= 35 => "Hot",
                _ => "Scorching"
            };
        }
    }
}
