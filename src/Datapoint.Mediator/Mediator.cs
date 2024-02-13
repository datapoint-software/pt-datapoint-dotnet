using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.Mediator
{
    internal sealed class Mediator : IMediator
    {
        private readonly Func<IServiceProvider, IMiddleware>[] _middlewareFactories;

        private readonly IServiceProvider _services;

        internal Mediator(Func<IServiceProvider, IMiddleware>[] middlewareFactories, IServiceProvider services)
        {
            _middlewareFactories = middlewareFactories;
            _services = services;
        }

        public Task HandleCommandAsync<TCommand>(TCommand command, CancellationToken ct) where TCommand : ICommand
        {
            Func<TCommand, Task> next = (command) => _services
                .GetRequiredService<ICommandHandler<TCommand>>()
                .HandleCommandAsync(command, ct);

            var i = _middlewareFactories.Length;

            while (i > 0)
            {
                var current = next;
                var middleware = _middlewareFactories[--i].Invoke(_services);

                next = (command) => middleware.HandleCommandAsync(
                    command,
                    current,
                    ct);
            }

            return next(command);
        }

        public Task<TCommandResult> HandleCommandAsync<TCommand, TCommandResult>(TCommand command, CancellationToken ct) where TCommand : ICommand<TCommandResult>
        {
            Func<TCommand, Task<TCommandResult>> next = (command) => _services
                .GetRequiredService<ICommandHandler<TCommand, TCommandResult>>()
                .HandleCommandAsync(command, ct);

            var i = _middlewareFactories.Length;

            while (i > 0)
            {
                var current = next;
                var middleware = _middlewareFactories[--i].Invoke(_services);

                next = (command) => middleware.HandleCommandAsync(
                    command,
                    current,
                    ct);
            }

            return next(command);
        }
        
        public Task HandleMessageAsync<TMessage>(TMessage message, CancellationToken ct) where TMessage : IMessage
        {
            Func<TMessage, Task> next = (message) => _services
                .GetRequiredService<IMessageHandler<TMessage>>()
                .HandleMessageAsync(message, ct);

            var i = _middlewareFactories.Length;

            while (i > 0)
            {
                var current = next;
                var middleware = _middlewareFactories[--i].Invoke(_services);

                next = (message) => middleware.HandleMessageAsync(
                    message,
                    current,
                    ct);
            }

            return next(message);
        }
        
        public Task<TQueryResult> HandleQueryAsync<TQuery, TQueryResult>(TQuery query, CancellationToken ct) where TQuery : IQuery<TQueryResult>
        {
            Func<TQuery, Task<TQueryResult>> next = (query) => _services
                .GetRequiredService<IQueryHandler<TQuery, TQueryResult>>()
                .HandleQueryAsync(query, ct);

            var i = _middlewareFactories.Length;

            while (i > 0)
            {
                var current = next;
                var middleware = _middlewareFactories[--i].Invoke(_services);

                next = (query) => middleware.HandleQueryAsync(
                    query,
                    current,
                    ct);
            }

            return next(query);
        }
    }
}
