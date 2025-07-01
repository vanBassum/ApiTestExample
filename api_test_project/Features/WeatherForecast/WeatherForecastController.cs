using ApiExample.Data;
using ApiExample.Infrastructure.Mapping;
using ApiExample.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.Features.WeatherForecast
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper<WeatherForecastDto, WeatherForecastEntity> _mapper;

        public WeatherForecastController(AppDbContext context, IMapper<WeatherForecastDto, WeatherForecastEntity> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<WeatherForecastDto>>> GetAll([FromQuery] WeatherForecastQueryParameters queryParams)
        {
            var query = _context.Forecasts
                .AsNoTracking()
                .FilterBy(queryParams);

            var totalCount = await query.CountAsync();

            var entities = await query
                .SortBy(queryParams)
                .Paginate(queryParams)
                .ToListAsync();

            var items = entities
                .Select(_mapper.ToDto)
                .ToList();

            var result = new PagedResult<WeatherForecastDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = queryParams.Page,
                PageSize = queryParams.PageSize
            };

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<WeatherForecastDto>> GetById(int id)
        {
            var entity = await _context.Forecasts.FindAsync(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.ToDto(entity));
        }

        [HttpPost]
        public async Task<ActionResult<WeatherForecastDto>> Create([FromBody] WeatherForecastDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = _mapper.ToEntity(dto);
            await _context.Forecasts.AddAsync(entity);
            await _context.SaveChangesAsync();

            var createdDto = _mapper.ToDto(entity);
            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<WeatherForecastDto>> Update(int id, [FromBody] WeatherForecastDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _context.Forecasts.FindAsync(id);
            if (entity == null)
                return NotFound();

            _mapper.UpdateEntity(dto, entity);
            await _context.SaveChangesAsync();

            return Ok(_mapper.ToDto(entity));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Forecasts.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.Forecasts.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
