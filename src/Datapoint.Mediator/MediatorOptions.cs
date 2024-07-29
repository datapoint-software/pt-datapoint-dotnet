using Microsoft.Extensions.DependencyInjection;
using System;

namespace Datapoint.Mediator
{
    internal sealed class MediatorOptions
    {
        internal MediatorOptions(Func<IServiceProvider, IMiddleware>[] middlewareFactories, ServiceLifetime serviceLifetime)
        {
            MiddlewareFactories = middlewareFactories;
            ServiceLifetime = serviceLifetime;
        }

        public Func<IServiceProvider, IMiddleware>[] MiddlewareFactories { get; }

        public ServiceLifetime ServiceLifetime { get; }
    }
}
