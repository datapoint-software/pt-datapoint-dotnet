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
        /// <typeparam name="TEntityFrameworkCoreContext">The Entity Framework Core context type.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <param name="configure">The configuration action.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddEntityFrameworkCoreUnitOfWork<TEntityFrameworkCoreUnitOfWorkDefinition, TEntityFrameworkCoreUnitOfWork, TEntityFrameworkCoreContext>(this IServiceCollection services, Action<EntityFrameworkCoreUnitOfWorkOptionsBuilder<TEntityFrameworkCoreContext>>? configure)
            where TEntityFrameworkCoreUnitOfWorkDefinition : IUnitOfWork
            where TEntityFrameworkCoreUnitOfWork : EntityFrameworkCoreUnitOfWork<TEntityFrameworkCoreContext>
            where TEntityFrameworkCoreContext : EntityFrameworkCoreContext
        {
            var optionsBuilder = new EntityFrameworkCoreUnitOfWorkOptionsBuilder<TEntityFrameworkCoreContext>(services);

            configure?.Invoke(optionsBuilder);

            var options = optionsBuilder.BuildOptions();

            services.AddDbContextFactory<TEntityFrameworkCoreContext>(
                (context) =>
                {
                    options.ContextConfiguration?.Invoke((DbContextOptionsBuilder<TEntityFrameworkCoreContext>) context);
                },
                options.ServiceLifetime);

            services.Add(
                new ServiceDescriptor(
                    typeof(TEntityFrameworkCoreUnitOfWorkDefinition),
                    typeof(TEntityFrameworkCoreUnitOfWork),
                    options.ServiceLifetime));

            return services;
        }
    }
}
