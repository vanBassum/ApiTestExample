using System.ComponentModel.DataAnnotations;

namespace ApiExample.Infrastructure.Mapping
{
    public interface IMapper<TDto, TEntity>
    {
        TDto ToDto(TEntity entity);
        TEntity ToEntity(TDto dto);
        void UpdateEntity(TDto dto, TEntity entity);
        void UpdateDto(TEntity entity, TDto dto);
    }


}
