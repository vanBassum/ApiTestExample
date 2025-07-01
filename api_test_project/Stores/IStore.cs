
using ApiExample.Models;

namespace ApiExample.Stores
{
    public interface IStore<TDto, TQueryParams>
    {
        Task<List<TDto>> GetAllAsync(TQueryParams queryParams);
        Task<TDto?> GetByIdAsync(int id);
        Task<TDto> CreateAsync(TDto dto);
        Task<TDto> UpdateAsync(int id, TDto dto);
        Task DeleteAsync(int id);
    }




}
