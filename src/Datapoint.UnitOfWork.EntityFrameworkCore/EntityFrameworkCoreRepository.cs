namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    /// <inheritdoc />
    public abstract class EntityFrameworkCoreRepository<TEntityFrameworkCoreUnitOfWork, TEntity> : EntityFrameworkCoreReadOnlyRepository<TEntityFrameworkCoreUnitOfWork, TEntity>, IRepository<TEntity>
        where TEntityFrameworkCoreUnitOfWork : EntityFrameworkCoreUnitOfWork
        where TEntity : class, IEntity
    {
        /// <inheritdoc />
        protected EntityFrameworkCoreRepository(TEntityFrameworkCoreUnitOfWork unitOfWork) : base(unitOfWork)
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
