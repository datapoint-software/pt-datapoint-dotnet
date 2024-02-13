using System;

namespace Datapoint.Mediator
{
    /// <inheritdoc />
    public abstract class Command : ICommand
    {
        /// <inheritdoc />
        public Command()
        {
            Creation = DateTimeOffset.Now;
        }

        /// <inheritdoc />
        public Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public DateTimeOffset Creation { get; }
    }

    /// <inheritdoc />
    public abstract class Command<TCommandResult> : ICommand<TCommandResult>
    {
        /// <inheritdoc />
        public Command()
        {
            Creation = DateTimeOffset.Now;
        }

        /// <inheritdoc />
        public Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public DateTimeOffset Creation { get; }
    }
}
