namespace ApiExample.Queries
{
    public interface IQueryBuilder<TParams, TEntity>
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> source, TParams parameters);
    }


}
