using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;

namespace Datapoint.Mediator
{
    /// <summary>
    /// A mediator options builder.
    /// </summary>
    public sealed class MediatorOptionsBuilder
    {
        private readonly List<Func<IServiceProvider, IMiddleware>> _middlewareFactories = [];

        internal MediatorOptionsBuilder(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        /// Gets the services collection this options builder belongs to.
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Gets or sets the service lifetime.
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Transient;

        /// <summary>
        /// Adds all handlers from a types assembly with a transient lifetime.
        /// </summary>
        /// <typeparam name="T">A type from the assembly to add the handlers from.</typeparam>
        /// <returns>The mediator options builder.</returns>
        public MediatorOptionsBuilder AddHandlersFromAssemblyOf<T>() where T : class =>

            AddHandlersFromAssemblyOf<T>(ServiceLifetime.Transient);

        /// <summary>
        /// Adds all handlers from a types assembly.
        /// </summary>
        /// <typeparam name="T">A type from the assembly to add the handlers from.</typeparam>
        /// <param name="serviceLifetime">The handlers service lifetime.</param>
        /// <returns>The mediator options builder.</returns>
        public MediatorOptionsBuilder AddHandlersFromAssemblyOf<T>(ServiceLifetime serviceLifetime) where T : class
        {
            foreach (var implementationType in typeof(T).Assembly.GetTypes())
            {
                if (implementationType.IsClass && implementationType.IsPublic)
                {
                    if (implementationType.IsAbstract || implementationType.IsGenericType)
                        continue;

                    foreach (var interfaceType in implementationType.GetInterfaces())
                    {
                        if (!interfaceType.IsGenericType)
                            continue;

                        var interfaceGenericType = interfaceType.GetGenericTypeDefinition();

                        if (interfaceGenericType == typeof(ICommandHandler<>) ||
                            interfaceGenericType == typeof(ICommandHandler<,>) ||
                            interfaceGenericType == typeof(IMessageHandler<>) ||
                            interfaceGenericType == typeof(IQueryHandler<,>))
                        {
                            Services.TryAdd(
                                new ServiceDescriptor(
                                    interfaceType,
                                    implementationType,
                                    serviceLifetime));
                        }
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// Adds a middleware to the mediator pipeline.
        /// 
        /// Please note this will result in the middleware type being registered
        /// against the domain service collection with a transient lifetime.
        /// </summary>
        /// <typeparam name="TMiddleware">The middleware type.</typeparam>
        /// <returns>The mediator options builder.</returns>
        public MediatorOptionsBuilder AddMiddleware<TMiddleware>() where TMiddleware : class, IMiddleware =>

            AddMiddleware<TMiddleware>(ServiceLifetime.Transient);

        /// <summary>
        /// Adds a middleware to the mediator pipeline.
        /// 
        /// Please note this will result in the middleware type being registered
        /// against the domain service collection.
        /// </summary>
        /// <typeparam name="TMiddleware">The middleware type.</typeparam>
        /// <param name="serviceLifetime">The middleware service lifetime.</param>
        /// <returns>The mediator options builder.</returns>
        public MediatorOptionsBuilder AddMiddleware<TMiddleware>(ServiceLifetime serviceLifetime) where TMiddleware : class, IMiddleware
        {
            Services.TryAdd(new ServiceDescriptor(typeof(TMiddleware), typeof(TMiddleware), serviceLifetime));
            _middlewareFactories.Add((services) => services.GetRequiredService<TMiddleware>());

            return this;
        }

        /// <summary>
        /// Adds a middleware to the mediator pipeline.
        /// 
        /// Please note this will result in the middleware type being registered
        /// against the domain service collection with a transient lifetime.
        /// </summary>
        /// <typeparam name="TMiddleware">The middleware type.</typeparam>
        /// <param name="middlewareFactory">The middleware factory.</param>
        /// <returns>The mediator options builder.</returns>
        public MediatorOptionsBuilder AddMiddleware<TMiddleware>(Func<IServiceProvider, TMiddleware> middlewareFactory) where TMiddleware : class, IMiddleware =>

            AddMiddleware(middlewareFactory, ServiceLifetime.Transient);

        /// <summary>
        /// Adds a middleware to the mediator pipeline.
        /// 
        /// Please note this will result in the middleware type being registered
        /// against the domain service collection.
        /// </summary>
        /// <typeparam name="TMiddleware">The middleware type.</typeparam>
        /// <param name="middlewareFactory">The middleware factory.</param>
        /// <param name="serviceLifetime">The middleware service lifetime.</param>
        /// <returns>The mediator options builder.</returns>
        public MediatorOptionsBuilder AddMiddleware<TMiddleware>(Func<IServiceProvider, TMiddleware> middlewareFactory, ServiceLifetime serviceLifetime) where TMiddleware : class, IMiddleware
        {
            Services.TryAdd(new ServiceDescriptor(typeof(TMiddleware), middlewareFactory, serviceLifetime));
            _middlewareFactories.Add((services) => services.GetRequiredService<TMiddleware>());

            return this;
        }

        internal MediatorOptions BuildOptions() =>
            
            new ([.. _middlewareFactories], ServiceLifetime);
    }
}
