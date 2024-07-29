using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Datapoint.Mediator.FluentValidation
{
    /// <summary>
    /// Options builder for the fluent validation middleware.
    /// </summary>
    public sealed class FluentValidationMiddlewareOptionsBuilder
    {
        private readonly MediatorOptionsBuilder _mediatorOptionsBuilder;

        internal FluentValidationMiddlewareOptionsBuilder(MediatorOptionsBuilder mediatorOptionsBuilder)
        {
            _mediatorOptionsBuilder = mediatorOptionsBuilder;
        }

        /// <summary>
        /// Gets or sets the middleware service lifetime.
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Transient;

        /// <summary>
        /// Adds all validators from a types assembly with a transient lifetime.
        /// </summary>
        /// <typeparam name="T">A type from the assembly to add the validators from.</typeparam>
        /// <returns>The fluent validation middleware options builder.</returns>
        public FluentValidationMiddlewareOptionsBuilder AddValidatorsFromAssemblyOf<T>() where T : class =>

            AddValidatorsFromAssemblyOf<T>(ServiceLifetime.Transient);

        /// <summary>
        /// Adds all validators from a types assembly.
        /// </summary>
        /// <typeparam name="T">A type from the assembly to add the validators from.</typeparam>
        /// <param name="serviceLifetime">The validator service lifetime.</param>
        /// <returns>The fluent validation middleware options builder.</returns>
        public FluentValidationMiddlewareOptionsBuilder AddValidatorsFromAssemblyOf<T>(ServiceLifetime serviceLifetime) where T : class
        {
            Type? parentImplementationType;

            foreach (var implementationType in typeof(T).Assembly.GetTypes())
            {
                if (implementationType.IsClass && implementationType.IsPublic)
                {
                    if (implementationType.IsAbstract || implementationType.IsGenericType)
                        continue;

                    parentImplementationType = implementationType;

                    while ((parentImplementationType = parentImplementationType!.BaseType) is not null)
                    {
                        if (!parentImplementationType.IsGenericType)
                            continue;

                        var parentImplementationGenericType = parentImplementationType.GetGenericTypeDefinition();

                        if (parentImplementationGenericType == typeof(AbstractValidator<>))
                        {
                            _mediatorOptionsBuilder.Services.TryAdd(
                                new ServiceDescriptor(
                                    parentImplementationType,
                                    implementationType,
                                    serviceLifetime));
                        }
                    }
                }
            }

            return this;
        }

        internal FluentValidationMiddlewareOptions BuildOptions() =>

            new (ServiceLifetime);
    }
}
