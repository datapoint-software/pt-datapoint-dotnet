using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.Mediator
{
    /// <summary>
    /// A command handler.
    /// </summary>
    /// <typeparam name="TCommand">The command type.</typeparam>
    public interface ICommandHandler<TCommand>
    {
        /// <summary>
        /// Handles a command asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the command result.</returns>
        Task HandleCommandAsync(TCommand command, CancellationToken ct);
    }

    /// <summary>
    /// A command handler.
    /// </summary>
    /// <typeparam name="TCommand">The command type.</typeparam>
    /// <typeparam name="TCommandResult">The command result type.</typeparam>
    public interface ICommandHandler<TCommand, TCommandResult> where TCommand : ICommand<TCommandResult>
    {
        /// <summary>
        /// Handles a command asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the command result.</returns>
        Task<TCommandResult> HandleCommandAsync(TCommand command, CancellationToken ct);
    }
}
