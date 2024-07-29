using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.Mediator
{
    /// <summary>
    /// A query handler.
    /// </summary>
    /// <typeparam name="TQuery">The query type.</typeparam>
    /// <typeparam name="TQueryResult">The query result type.</typeparam>
    public interface IQueryHandler<TQuery, TQueryResult> where TQuery : IQuery<TQueryResult>
    {
        /// <summary>
        /// Handles a query asynchronously.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the query result.</returns>
        Task<TQueryResult> HandleQueryAsync(TQuery query, CancellationToken ct);
    }
}
