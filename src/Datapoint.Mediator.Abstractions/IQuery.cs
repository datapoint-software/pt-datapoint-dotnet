using System;

namespace Datapoint.Mediator
{
    /// <summary>
    /// A query.
    /// </summary>
    /// <typeparam name="TQueryResult">The query result type.</typeparam>
    public interface IQuery<TQueryResult>
    {
        /// <summary>
        /// The query unique identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// The query creation date and time.
        /// </summary>
        public DateTimeOffset Creation { get; }
    }
}
