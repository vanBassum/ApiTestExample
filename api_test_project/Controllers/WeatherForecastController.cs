using api_test_project.Data;
using api_test_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_test_project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApplicationDbContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await _context.Forecasts.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WeatherForecast forecast)
        {
            if (forecast == null)
            {
                return BadRequest("Forecast cannot be null.");
            }
            _context.Forecasts.Add(forecast);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = forecast.Id }, forecast);
        }
    }
}
