
using ApiExample.Models;

namespace ApiExample.Stores
{
    public interface IStore<TDto, TQueryBuilder>
    {
        Task<List<WeatherForecast>> GetAllAsync(TQueryBuilder queryBuilder);
        Task<TDto?> GetByIdAsync(int id);
        Task<TDto> CreateAsync(TDto dto);
        Task<TDto> UpdateAsync(int id, TDto dto);
        Task DeleteAsync(int id);
    }




}
