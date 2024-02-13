using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    internal sealed class EntityFrameworkCoreUnitOfWorkOptions<TEntityFrameworkCoreContext>
        where TEntityFrameworkCoreContext : EntityFrameworkCoreContext
    {
        public EntityFrameworkCoreUnitOfWorkOptions(Action<DbContextOptionsBuilder<TEntityFrameworkCoreContext>>? contextConfiguration, ServiceLifetime serviceLifetime)
        {
            ContextConfiguration = contextConfiguration;
            ServiceLifetime = serviceLifetime;
        }

        public Action<DbContextOptionsBuilder<TEntityFrameworkCoreContext>>? ContextConfiguration { get; }

        public ServiceLifetime ServiceLifetime { get; }
    }
}
