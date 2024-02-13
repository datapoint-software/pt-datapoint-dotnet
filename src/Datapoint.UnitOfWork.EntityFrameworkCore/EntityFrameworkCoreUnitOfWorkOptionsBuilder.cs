using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    /// <summary>
    /// An Entity Framework Core unit of work options builder.
    /// </summary>
    public sealed class EntityFrameworkCoreUnitOfWorkOptionsBuilder<TEntityFrameworkCoreContext> 
        where TEntityFrameworkCoreContext : EntityFrameworkCoreContext
    {
        private Action<DbContextOptionsBuilder<TEntityFrameworkCoreContext>>? _contextConfiguration;

        /// <summary>
        /// Creates a new Entity Framework Core unit of work options builder.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public EntityFrameworkCoreUnitOfWorkOptionsBuilder(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        /// Gets or sets the unit of work service lifetime.
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Scoped;

        /// <summary>
        /// Gets the service collection.
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Adds repositories from an assembly where the given type is defined
        /// with a scoped service lifetime.
        /// </summary>
        /// <typeparam name="T">The type of which to get the assembly repositories from.</typeparam>
        /// <returns>The Entity Framework Core unit of work options builder.</returns>
        public EntityFrameworkCoreUnitOfWorkOptionsBuilder<TEntityFrameworkCoreContext> AddRepositoriesFromAssemblyOf<T>() where T : class =>

            AddRepositoriesFromAssemblyOf<T>(ServiceLifetime.Scoped);

        /// <summary>
        /// Adds repositories from an assembly where the given type is defined.
        /// </summary>
        /// <typeparam name="T">The type of which to get the assembly repositories from.</typeparam>
        /// <param name="serviceLifetime">The repositories service lifetime.</param>
        /// <returns>The Entity Framework Core unit of work options builder.</returns>
        public EntityFrameworkCoreUnitOfWorkOptionsBuilder<TEntityFrameworkCoreContext> AddRepositoriesFromAssemblyOf<T>(ServiceLifetime serviceLifetime) where T : class
        {
            foreach (var implementationType in typeof(T).Assembly.GetTypes())
            {
                if (implementationType.IsClass && implementationType.IsPublic)
                {
                    if (implementationType.IsAbstract || implementationType.IsGenericType)
                        continue;

                    foreach (var interfaceType in implementationType.GetInterfaces())
                    {
                        if (interfaceType.IsGenericType)
                        {
                            var interfaceGenericType = interfaceType.GetGenericTypeDefinition();

                            if (interfaceGenericType == typeof(IReadOnlyRepository<>) ||
                                interfaceGenericType == typeof(IRepository<>))
                            {
                                Services.Add(
                                    new ServiceDescriptor(
                                        interfaceType,
                                        implementationType,
                                        serviceLifetime));
                            }
                        }
                        else
                        {
                            foreach (var innerInterfaceTypes in interfaceType.GetInterfaces())
                            {
                                if (innerInterfaceTypes.IsGenericType)
                                {
                                    var innerInterfaceGenericType = innerInterfaceTypes.GetGenericTypeDefinition();

                                    if (innerInterfaceGenericType == typeof(IReadOnlyRepository<>) ||
                                        innerInterfaceGenericType == typeof(IRepository<>))
                                    {
                                        Services.Add(
                                            new ServiceDescriptor(
                                                interfaceType,
                                                implementationType,
                                                serviceLifetime));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// Adds workers from an assembly where the given type is defined
        /// with a transient service lifetime.
        /// </summary>
        /// <typeparam name="T">The type of which to get the assembly workers from.</typeparam>
        /// <returns>The Entity Framework Core unit of work options builder.</returns>
        public EntityFrameworkCoreUnitOfWorkOptionsBuilder<TEntityFrameworkCoreContext> AddWorkersFromAssemblyOf<T>() where T : class =>

            AddWorkersFromAssemblyOf<T>(ServiceLifetime.Transient);

        /// <summary>
        /// Adds workers from an assembly where the given type is defined.
        /// </summary>
        /// <typeparam name="T">The type of which to get the assembly workers from.</typeparam>
        /// <param name="serviceLifetime">The workers service lifetime.</param>
        /// <returns>The Entity Framework Core unit of work options builder.</returns>
        public EntityFrameworkCoreUnitOfWorkOptionsBuilder<TEntityFrameworkCoreContext> AddWorkersFromAssemblyOf<T>(ServiceLifetime serviceLifetime) where T : class
        {
            foreach (var implementationType in typeof(T).Assembly.GetTypes())
            {
                if (implementationType.IsClass && implementationType.IsPublic)
                {
                    if (implementationType.IsAbstract || implementationType.IsGenericType)
                        continue;

                    foreach (var interfaceType in implementationType.GetInterfaces())
                    {
                        foreach (var innerInterfaceType in interfaceType.GetInterfaces())
                        {
                            if (innerInterfaceType == typeof(IWorker))
                            {
                                Services.Add(
                                    new ServiceDescriptor(
                                        interfaceType,
                                        implementationType,
                                        serviceLifetime));
                            }
                        }
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// Registers a context options configuration action.
        /// </summary>
        /// <param name="configure">The configuration action.</param>
        /// <returns>The Entity Framework Core unit of work options builder.</returns>
        public EntityFrameworkCoreUnitOfWorkOptionsBuilder<TEntityFrameworkCoreContext> UseContextConfiguration(Action<DbContextOptionsBuilder<TEntityFrameworkCoreContext>>? configure)
        {
            _contextConfiguration = configure;

            return this;
        }

        internal EntityFrameworkCoreUnitOfWorkOptions<TEntityFrameworkCoreContext> BuildOptions() => 
            
            new EntityFrameworkCoreUnitOfWorkOptions<TEntityFrameworkCoreContext>(
                _contextConfiguration,
                ServiceLifetime);
    }
}
