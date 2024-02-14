namespace Datapoint.UnitOfWork
{
    /// <summary>
    /// A read only repository.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
    }
}
