using System;

namespace Datapoint.Mediator
{
    /// <summary>
    /// A command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// The command unique identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// The command creation date and time.
        /// </summary>
        public DateTimeOffset Creation { get; }
    }

    /// <summary>
    /// A command.
    /// </summary>
    /// <typeparam name="TCommandResult">The command result type.</typeparam>
    public interface ICommand<TCommandResult>
    {
        /// <summary>
        /// The command unique identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// The command creation date and time.
        /// </summary>
        public DateTimeOffset Creation { get; }
    }
}
