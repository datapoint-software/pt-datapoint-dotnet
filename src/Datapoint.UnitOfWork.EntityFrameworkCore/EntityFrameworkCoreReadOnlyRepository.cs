using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    /// <summary>
    /// An Entity Framework Core repository.
    /// </summary>
    /// <typeparam name="TEntityFrameworkCoreUnitOfWork">The unit of work type.</typeparam>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public abstract class EntityFrameworkCoreReadOnlyRepository<TEntityFrameworkCoreUnitOfWork, TEntity> : IReadOnlyRepository<TEntity> 
        where TEntityFrameworkCoreUnitOfWork : EntityFrameworkCoreUnitOfWork
        where TEntity : class, IEntity
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

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<TEntity>> FindAllAsync(CancellationToken ct) =>

            await Entities.ToListAsync(ct);

        /// <inheritdoc />
        public Task<TEntity?> FindByIdAsync(long id, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.Id == id, ct);

        /// <inheritdoc />
        public Task<TEntity?> FindByPublicIdAsync(Guid publicId, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.PublicId == publicId, ct);
    }
}
