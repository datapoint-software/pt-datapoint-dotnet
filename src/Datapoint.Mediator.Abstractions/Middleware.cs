using System;
using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.Mediator
{
    /// <inheritdoc />
    public abstract class Middleware : IMiddleware
    {
        /// <inheritdoc />
        public virtual Task HandleCommandAsync<TCommand>(TCommand command, Func<TCommand, Task> next, CancellationToken ct) where TCommand : ICommand =>

            next(command);

        /// <inheritdoc />
        public virtual Task<TCommandResult> HandleCommandAsync<TCommand, TCommandResult>(TCommand command, Func<TCommand, Task<TCommandResult>> next, CancellationToken ct) where TCommand : ICommand<TCommandResult> =>

            next(command);

        /// <inheritdoc />
        public virtual Task HandleMessageAsync<TMessage>(TMessage message, Func<TMessage, Task> next, CancellationToken ct) where TMessage : IMessage =>

            next(message);

        /// <inheritdoc />
        public virtual Task<TQueryResult> HandleQueryAsync<TQuery, TQueryResult>(TQuery query, Func<TQuery, Task<TQueryResult>> next, CancellationToken ct) where TQuery : IQuery<TQueryResult> =>

            next(query);
    }
}
