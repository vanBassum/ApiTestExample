using ApiExample.Data;
using ApiExample.Data.Entities;
using ApiExample.Models;
using ApiExample.Queries;
using ApiExample.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IStore<WeatherForecast, WeatherForecastEntity> _repository;

        public WeatherForecastController(IStore<WeatherForecast, WeatherForecastEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<WeatherForecast>>> GetAll([FromQuery] WeatherForecastQuery query)
        {
            var result = await _repository.GetAllAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecast>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            if (dto == null)
                return NotFound();

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<WeatherForecast>> Create(WeatherForecast dto)
        {
            var created = await _repository.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WeatherForecast>> Update(int id, WeatherForecast dto)
        {
            var updated = await _repository.UpdateAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
