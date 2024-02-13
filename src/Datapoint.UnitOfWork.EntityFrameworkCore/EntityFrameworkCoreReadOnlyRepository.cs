using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    /// <summary>
    /// An Entity Framework Core repository.
    /// </summary>
    /// <typeparam name="TEntityFrameworkCoreContext">The Entity Framework Core context type.</typeparam>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public abstract class EntityFrameworkCoreReadOnlyRepository<TEntityFrameworkCoreContext, TEntity> : IReadOnlyRepository<TEntity> 
        where TEntityFrameworkCoreContext : EntityFrameworkCoreContext
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Creates a new entity framework core repository.
        /// </summary>
        /// <param name="context">The unit of work context.</param>
        protected EntityFrameworkCoreReadOnlyRepository(TEntityFrameworkCoreContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        protected TEntityFrameworkCoreContext Context { get; }

        /// <summary>
        /// Gets the entities set.
        /// </summary>
        protected DbSet<TEntity> Entities => Context.Set<TEntity>();

        /// <inheritdoc />
        public Task<bool> AnyByIdAsync(long id, CancellationToken ct) =>

            Entities.AnyAsync(e => e.Id == id, ct);

        /// <inheritdoc />
        public Task<bool> AnyByPublicIdAsync(Guid publicId, CancellationToken ct) =>

            Entities.AnyAsync(e => e.PublicId == publicId, ct);

        /// <inheritdoc />
        public Task<TEntity> GetByIdAsync(long id, CancellationToken ct) =>

            Entities.FirstAsync(e => e.Id == id, ct);

        /// <inheritdoc />
        public Task<TEntity> GetByPublicIdAsync(Guid publicId, CancellationToken ct) =>

            Entities.FirstAsync(e => e.PublicId == publicId, ct);
    }
}
