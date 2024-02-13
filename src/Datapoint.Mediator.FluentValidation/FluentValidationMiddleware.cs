using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

using FluentValidationException = global::FluentValidation.ValidationException;
using ValidationException = global::Datapoint.ValidationException;

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

        private async Task ValidateAndThrowAsync<TAction>(TAction action, CancellationToken ct)
        {
            var validator = _services.GetService<AbstractValidator<TAction>>();

            if (validator is not null)
            {
                try
                {
                    await validator.ValidateAndThrowAsync(action, ct);
                }
                catch (FluentValidationException e)
                {
                    var validationException = new ValidationException(e.Message, e);

                    validationException.WithErrorCode("FVMTEX");

                    foreach (var error in e.Errors)
                        validationException.WithInnerError(
                            error.PropertyName, 
                            error.ErrorCode.ToLower(), 
                            error.ErrorMessage);

                    throw validationException;
                }
            }
        }
    }
}
