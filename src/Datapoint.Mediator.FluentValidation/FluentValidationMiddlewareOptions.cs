using Microsoft.Extensions.DependencyInjection;

namespace Datapoint.Mediator.FluentValidation
{
    internal sealed class FluentValidationMiddlewareOptions
    {
        internal FluentValidationMiddlewareOptions(ServiceLifetime serviceLifetime)
        {
            ServiceLifetime = serviceLifetime;
        }

        public ServiceLifetime ServiceLifetime { get; }
    }
}