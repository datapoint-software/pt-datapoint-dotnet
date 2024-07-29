using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

using FluentValidationException = global::FluentValidation.ValidationException;

namespace Datapoint.Mediator.FluentValidation
{
    internal sealed class FluentValidationMiddleware : IMiddleware
    {
        private readonly IServiceProvider _services;

        internal FluentValidationMiddleware(IServiceProvider services)
        {
            _services = services;
        }

        public async Task HandleCommandAsync<TCommand>(TCommand command, Func<TCommand, Task> next, CancellationToken ct) where TCommand : ICommand
        {
            await ValidateAndThrowAsync(command, ct);

            await next(command);
        }

        public async Task<TCommandResult> HandleCommandAsync<TCommand, TCommandResult>(TCommand command, Func<TCommand, Task<TCommandResult>> next, CancellationToken ct) where TCommand : ICommand<TCommandResult>
        {
            await ValidateAndThrowAsync(command, ct);

            return await next(command);
        }

        public async Task HandleMessageAsync<TMessage>(TMessage message, Func<TMessage, Task> next, CancellationToken ct) where TMessage : IMessage
        {
            await ValidateAndThrowAsync(message, ct);

            await next(message);
        }

        public async Task<TQueryResult> HandleQueryAsync<TQuery, TQueryResult>(TQuery query, Func<TQuery, Task<TQueryResult>> next, CancellationToken ct) where TQuery : IQuery<TQueryResult>
        {
            await ValidateAndThrowAsync(query, ct);

            return await next(query);
        }

        private async Task ValidateAndThrowAsync<T>(T subject, CancellationToken ct)
        {
            var validator = _services.GetService<AbstractValidator<T>>();

            if (validator is not null)
            {
                try
                {
                    await validator.ValidateAndThrowAsync(subject, ct);
                }
                catch (FluentValidationException e)
                {
                    var validationException = new ValidationException(
                        "A validation exception has been thrown.", 
                        e);

                    foreach (var error in e.Errors)
                        validationException.AddInnerError(new Error(
                            error.ErrorCode.ToLower(),
                            error.PropertyName,
                            error.ErrorMessage));

                    throw validationException;
                }
            }
        }
    }
}
