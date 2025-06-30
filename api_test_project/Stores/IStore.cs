using ApiExample.Queries;

namespace ApiExample.Stores
{
    public interface IStore<TDto, TEntity>
    {
        Task<List<TDto>> GetAllAsync(IQueryOptions<TEntity> queryOptions);
        Task<TDto?> GetByIdAsync(int id);
        Task<TDto> CreateAsync(TDto dto);
        Task<TDto> UpdateAsync(int id, TDto dto);
        Task DeleteAsync(int id);
    }







}
