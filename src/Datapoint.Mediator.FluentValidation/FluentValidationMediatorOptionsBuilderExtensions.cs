using System;

namespace Datapoint.Mediator.FluentValidation
{
    /// <summary>
    /// Service collection extensions for the fluent validation middleware.
    /// </summary>
    public static class FluentValidationMediatorOptionsBuilderExtensions
    {
        /// <summary>
        /// Adds a fluent validation middleware.
        /// </summary>
        /// <param name="mediatorOptionsBuilder">The mediator options builder.</param>
        /// <param name="configure">The configuration action.</param>
        /// <returns>The mediator options builder.</returns>
        public static MediatorOptionsBuilder AddFluentValidationMiddleware(this MediatorOptionsBuilder mediatorOptionsBuilder, Action<FluentValidationMiddlewareOptionsBuilder>? configure)
        {
            var optionsBuilder = new FluentValidationMiddlewareOptionsBuilder(mediatorOptionsBuilder);

            configure?.Invoke(optionsBuilder);

            var options = optionsBuilder.BuildOptions();

            mediatorOptionsBuilder.AddMiddleware(
                (services) => new FluentValidationMiddleware(services),
                options.ServiceLifetime);

            return mediatorOptionsBuilder;
        }
    }
}
