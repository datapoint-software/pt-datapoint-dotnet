namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    /// <inheritdoc />
    public abstract class EntityFrameworkCoreRepository<TEntityFrameworkCoreContext, TEntity> : EntityFrameworkCoreReadOnlyRepository<TEntityFrameworkCoreContext, TEntity>, IRepository<TEntity>
        where TEntityFrameworkCoreContext : EntityFrameworkCoreContext
        where TEntity : class, IEntity
    {
        /// <inheritdoc />
        protected EntityFrameworkCoreRepository(TEntityFrameworkCoreContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public TEntity Add(TEntity entity) =>

            Entities.Add(entity).Entity;

        /// <inheritdoc />
        public void Remove(TEntity entity) =>

            Entities.Remove(entity);
    }
}
