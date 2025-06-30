namespace ApiExample.Queries
{
    public interface IQueryOptions<TEntity>
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> query);
    }

}
