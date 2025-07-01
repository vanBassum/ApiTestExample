using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Testing.TestUtils;
using ApiExample.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace Testing.Integration
{
    public class WeatherForecastControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public WeatherForecastControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_CreatesNewForecast()
        {
            var created = await CreateForecastAsync(new DateOnly(2025, 6, 30), 25);
            Assert.NotNull(created.Id);
            Assert.Equal(25, created.TemperatureC);
        }

        [Fact]
        public async Task GetById_ReturnsCorrectForecast()
        {
            var created = await CreateForecastAsync(new DateOnly(2025, 7, 1), 30);

            var result = await GetForecastByIdAsync(created.Id!.Value);
            Assert.NotNull(result);
            Assert.Equal(created.Id, result!.Id);
            Assert.Equal(30, result.TemperatureC);
        }

        [Fact]
        public async Task Put_UpdatesForecastSuccessfully()
        {
            var created = await CreateForecastAsync(new DateOnly(2025, 7, 2), 18);
            created.TemperatureC = 22;

            var response = await _client.PutAsJsonAsync($"/WeatherForecast/{created.Id}", created);
            response.EnsureSuccessStatusCode();

            var updated = await GetForecastByIdAsync(created.Id!.Value);
            Assert.Equal(22, updated!.TemperatureC);
        }

        [Fact]
        public async Task Delete_RemovesForecast()
        {
            var created = await CreateForecastAsync(new DateOnly(2025, 7, 3), 15);

            var deleteResponse = await _client.DeleteAsync($"/WeatherForecast/{created.Id}");
            Assert.Equal(System.Net.HttpStatusCode.NoContent, deleteResponse.StatusCode);

            var result = await _client.GetAsync($"/WeatherForecast/{created.Id}");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetAll_ReturnsAllForecasts()
        {
            var temps = new[] { 10, 20, 30, 40, 50 };
            foreach (var temp in temps)
            {
                await CreateForecastAsync(new DateOnly(2025, 6, temp / 2), temp);
            }

            var url = QueryHelpers.AddQueryString("/WeatherForecast", new Dictionary<string, string?>
            {
                ["SortBy"] = "TemperatureC",
                ["Descending"] = "true",
                ["Page"] = "0",
                ["PageSize"] = "2"
            });

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PagedResult<WeatherForecastDto>>();
            Assert.NotNull(result);
            Assert.Equal(2, result!.Items.Count);
            Assert.True(result.Items[0].TemperatureC > result.Items[1].TemperatureC);
        }

        private async Task<WeatherForecastDto> CreateForecastAsync(DateOnly date, int temperatureC)
        {
            var dto = new WeatherForecastDto
            {
                Date = date,
                TemperatureC = temperatureC
            };

            var response = await _client.PostAsJsonAsync("/WeatherForecast", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<WeatherForecastDto>() ?? throw new Exception("Failed to deserialize WeatherForecast");
        }

        private async Task<WeatherForecastDto?> GetForecastByIdAsync(int id)
        {
            var response = await _client.GetAsync($"/WeatherForecast/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<WeatherForecastDto>();
        }
    }
}
