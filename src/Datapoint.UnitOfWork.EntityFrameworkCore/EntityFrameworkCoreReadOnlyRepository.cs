using Microsoft.EntityFrameworkCore;

namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    /// <summary>
    /// An Entity Framework Core repository.
    /// </summary>
    /// <typeparam name="TEntityFrameworkCoreUnitOfWork">The unit of work type.</typeparam>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public abstract class EntityFrameworkCoreReadOnlyRepository<TEntityFrameworkCoreUnitOfWork, TEntity> : IReadOnlyRepository<TEntity> 
        where TEntityFrameworkCoreUnitOfWork : EntityFrameworkCoreUnitOfWork
        where TEntity : class
    {
        /// <summary>
        /// Creates a new entity framework core repository.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        protected EntityFrameworkCoreReadOnlyRepository(TEntityFrameworkCoreUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        protected DbSet<TEntity> Entities => UnitOfWork.Set<TEntity>();

        /// <summary>
        /// Gets the context.
        /// </summary>
        protected TEntityFrameworkCoreUnitOfWork UnitOfWork { get; }
    }
}
