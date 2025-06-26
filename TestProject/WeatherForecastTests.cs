using api_test_project;
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

        }
    }
}
