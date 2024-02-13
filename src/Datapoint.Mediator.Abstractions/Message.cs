using System;

namespace Datapoint.Mediator
{
    /// <inheritdoc />
    public abstract class Message : IMessage
    {
        /// <inheritdoc />
        public Message()
        {
            Creation = DateTimeOffset.Now;
        }

        /// <inheritdoc />
        public Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public DateTimeOffset Creation { get; }
    }
}
