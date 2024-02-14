using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    internal sealed class EntityFrameworkCoreUnitOfWorkOptions<TEntityFrameworkCoreUnitOfWork>
        where TEntityFrameworkCoreUnitOfWork : EntityFrameworkCoreUnitOfWork
    {
        public EntityFrameworkCoreUnitOfWorkOptions(Action<DbContextOptionsBuilder<TEntityFrameworkCoreUnitOfWork>>? contextConfiguration, ServiceLifetime serviceLifetime)
        {
            ContextConfiguration = contextConfiguration;
            ServiceLifetime = serviceLifetime;
        }

        public Action<DbContextOptionsBuilder<TEntityFrameworkCoreUnitOfWork>>? ContextConfiguration { get; }

        public ServiceLifetime ServiceLifetime { get; }
    }
}
