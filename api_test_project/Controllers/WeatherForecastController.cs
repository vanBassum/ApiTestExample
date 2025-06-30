using ApiExample.Data;
using ApiExample.Data.Entities;
using ApiExample.Models;
using ApiExample.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IStore<WeatherForecast, WeatherForecastQueryParameters> _store;

        public WeatherForecastController(IStore<WeatherForecast, WeatherForecastQueryParameters> store)
        {
            _store = store;
        }

        [HttpGet]
        public async Task<ActionResult<List<WeatherForecast>>> GetAll([FromQuery] WeatherForecastQueryParameters parameters)
        {;
            var result = await _store.GetAllAsync(parameters);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecast>> GetById(int id)
        {
            var dto = await _store.GetByIdAsync(id);
            if (dto == null)
                return NotFound();

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<WeatherForecast>> Create(WeatherForecast dto)
        {
            var created = await _store.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WeatherForecast>> Update(int id, WeatherForecast dto)
        {
            var updated = await _store.UpdateAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _store.DeleteAsync(id);
            return NoContent();
        }
    }
}
