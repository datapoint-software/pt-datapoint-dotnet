using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.UnitOfWork
{
    /// <summary>
    /// A unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all changes asynchronously.
        /// </summary>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task.</returns>
        Task SaveChangesAsync(CancellationToken ct);
    }
}
