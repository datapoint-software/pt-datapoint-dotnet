using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    /// <summary>
    /// Entity Framework Core unit of work service collection extensions.
    /// </summary>
    public static class EntityFrameworkCoreUnitOfWorkServiceCollectionExtensions
    {
        /// <summary>
        /// Adds an Entity Framework Core unit of work.
        /// </summary>
        /// <typeparam name="TEntityFrameworkCoreUnitOfWorkDefinition">The Entity Framework Core unit of work definition type.</typeparam>
        /// <typeparam name="TEntityFrameworkCoreUnitOfWork">The Entity Framework Core unit of work implementation type.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <param name="configure">The configuration action.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddEntityFrameworkCoreUnitOfWork<TEntityFrameworkCoreUnitOfWorkDefinition, TEntityFrameworkCoreUnitOfWork>(this IServiceCollection services, Action<EntityFrameworkCoreUnitOfWorkOptionsBuilder<TEntityFrameworkCoreUnitOfWork>>? configure)
            where TEntityFrameworkCoreUnitOfWorkDefinition : IUnitOfWork
            where TEntityFrameworkCoreUnitOfWork : EntityFrameworkCoreUnitOfWork, TEntityFrameworkCoreUnitOfWorkDefinition
        {
            var optionsBuilder = new EntityFrameworkCoreUnitOfWorkOptionsBuilder<TEntityFrameworkCoreUnitOfWork>(services);

            configure?.Invoke(optionsBuilder);

            var options = optionsBuilder.BuildOptions();

            services.AddDbContextFactory<TEntityFrameworkCoreUnitOfWork>(
                (context) =>
                {
                    options.ContextConfiguration?.Invoke((DbContextOptionsBuilder<TEntityFrameworkCoreUnitOfWork>) context);
                },
                options.ServiceLifetime);

            services.Add(
                new ServiceDescriptor(
                    typeof(TEntityFrameworkCoreUnitOfWorkDefinition),
                    (services) => services.GetRequiredService<TEntityFrameworkCoreUnitOfWork>(),
                    ServiceLifetime.Transient));

            return services;
        }
    }
}
