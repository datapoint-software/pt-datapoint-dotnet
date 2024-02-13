using System;

namespace Datapoint.Mediator
{
    /// <summary>
    /// A message.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// The message unique identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// The message creation date and time.
        /// </summary>
        public DateTimeOffset Creation { get; }
    }
}
