﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.Mediator
{
    /// <summary>
    /// A middleware.
    /// </summary>
    public interface IMiddleware
    {
        /// <summary>
        /// Handles a command asynchronously.
        /// </summary>
        /// <typeparam name="TCommand">The command type.</typeparam>
        /// <param name="command">The command.</param>
        /// <param name="next">The next callback in the middleware pipeline.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the command result.</returns>
        Task HandleCommandAsync<TCommand>(TCommand command, Func<TCommand, Task> next, CancellationToken ct)
            where TCommand : ICommand;

        /// <summary>
        /// Handles a command asynchronously.
        /// </summary>
        /// <typeparam name="TCommand">The command type.</typeparam>
        /// <typeparam name="TCommandResult">The command result type.</typeparam>
        /// <param name="command">The command.</param>
        /// <param name="next">The next callback in the middleware pipeline.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the command result.</returns>
        Task<TCommandResult> HandleCommandAsync<TCommand, TCommandResult>(TCommand command, Func<TCommand, Task<TCommandResult>> next, CancellationToken ct)
            where TCommand : ICommand<TCommandResult>;

        /// <summary>
        /// Handles a message asynchronously.
        /// </summary>
        /// <typeparam name="TMessage">The message type.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="next">The next callback in the middleware pipeline.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the message result.</returns>
        Task HandleMessageAsync<TMessage>(TMessage message, Func<TMessage, Task> next, CancellationToken ct)
            where TMessage : IMessage;

        /// <summary>
        /// Handles a query asynchronously.
        /// </summary>
        /// <typeparam name="TQuery">The query type.</typeparam>
        /// <typeparam name="TQueryResult">The query result type.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="next">The next callback in the middleware pipeline.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the query result.</returns>
        Task<TQueryResult> HandleQueryAsync<TQuery, TQueryResult>(TQuery query, Func<TQuery, Task<TQueryResult>> next, CancellationToken ct)
            where TQuery : IQuery<TQueryResult>;
    }
}
