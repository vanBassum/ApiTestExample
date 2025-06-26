using api_test_project.Models;
using System.Net.Http.Json;

namespace TestProject
{
    public class WeatherForecastTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public WeatherForecastTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_ReturnsWeatherForecasts()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode();
            var forecasts = await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
            Assert.NotNull(forecasts);
            Assert.Empty(forecasts);
        }

        [Fact]
        public async Task Post_AddsWeatherForecast()
        {
            // Arrange
            var forecast = new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = 25
            };
            // Act
            var response = await _client.PostAsJsonAsync("/WeatherForecast", forecast);
            // Assert
            response.EnsureSuccessStatusCode();
            var createdForecast = await response.Content.ReadFromJsonAsync<WeatherForecast>();
            Assert.NotNull(createdForecast);
            Assert.Equal(forecast.Date, createdForecast.Date);
            Assert.Equal(forecast.TemperatureC, createdForecast.TemperatureC);
        }
    }
}
