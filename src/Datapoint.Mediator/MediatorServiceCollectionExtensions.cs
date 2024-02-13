using Microsoft.Extensions.DependencyInjection;
using System;

namespace Datapoint.Mediator
{
    /// <summary>
    /// Mediator service collection extensions.
    /// </summary>
    public static class MediatorServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a mediator.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configure">The configuration action.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddMediator(this IServiceCollection services, Action<MediatorOptionsBuilder>? configure)
        {
            var optionsBuilder = new MediatorOptionsBuilder(services);

            configure?.Invoke(optionsBuilder);

            var options = optionsBuilder.BuildOptions();

            services.Add(
                new ServiceDescriptor(
                    typeof(IMediator),
                    (services) => new Mediator(
                        options.MiddlewareFactories,
                        services),
                    options.ServiceLifetime));

            return services;
        }
    }
}
