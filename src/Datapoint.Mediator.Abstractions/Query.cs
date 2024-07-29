using System;

namespace Datapoint.Mediator
{
    /// <inheritdoc />
    public abstract class Query<TQueryResult> : IQuery<TQueryResult>
    {
        /// <inheritdoc />
        public Query()
        {
            Creation = DateTimeOffset.Now;
        }

        /// <inheritdoc />
        public Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public DateTimeOffset Creation { get; }
    }
}
