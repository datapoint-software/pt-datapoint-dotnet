using System;
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
        /// Checks if an entity exists.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the result.</returns>
        Task<bool> AnyByIdAsync(long id, CancellationToken ct);

        /// <summary>
        /// Checks if an entity exists.
        /// </summary>
        /// <param name="publicId">The entity public identifier.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the result.</returns>
        Task<bool> AnyByPublicIdAsync(Guid publicId, CancellationToken ct);

        /// <summary>
        /// Gets an entity asynchronously.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the matching entity.</returns>
        Task<TEntity> GetByIdAsync(long id, CancellationToken ct);

        /// <summary>
        /// Gets an entity matching an identifier asynchronously.
        /// </summary>
        /// <param name="publicId">The entity public identifier.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the matching entity.</returns>
        Task<TEntity> GetByPublicIdAsync(Guid publicId, CancellationToken ct);
    }
}
