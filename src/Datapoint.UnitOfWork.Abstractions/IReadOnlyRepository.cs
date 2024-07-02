using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.UnitOfWork
{
    /// <summary>
    /// A read only repository.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IReadOnlyRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Finds all entities.
        /// </summary>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the matching entities.</returns>
        Task<IReadOnlyCollection<TEntity>> FindAllAsync(CancellationToken ct);

        /// <summary>
        /// Finds an entity by identifier.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the matching entity.</returns>
        Task<TEntity?> FindByIdAsync(long id, CancellationToken ct);

        /// <summary>
        /// Finds an entity by public identifier.
        /// </summary>
        /// <param name="publicId">The entity public identifier.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the matching entity.</returns>
        Task<TEntity?> FindByPublicIdAsync(Guid publicId, CancellationToken ct);
    }
}
