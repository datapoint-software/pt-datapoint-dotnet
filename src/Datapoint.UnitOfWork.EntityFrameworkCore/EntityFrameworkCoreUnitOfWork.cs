using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    /// <summary>
    /// An Entity Framework Core unit of work.
    /// </summary>
    /// <typeparam name="TEntityFrameworkCoreContext">The Entity Framework Core context type.</typeparam>
    public abstract class EntityFrameworkCoreUnitOfWork<TEntityFrameworkCoreContext> : IUnitOfWork where TEntityFrameworkCoreContext : EntityFrameworkCoreContext
    {
        /// <summary>
        /// Creates a new unit of work.
        /// </summary>
        /// <param name="context">The Entity Framework Core context.</param>
        public EntityFrameworkCoreUnitOfWork(TEntityFrameworkCoreContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        public TEntityFrameworkCoreContext Context { get; }

        /// <inheritdoc />
        public Task SaveChangesAsync(CancellationToken ct) => 
            
            Context.SaveChangesAsync(ct);
    }
}
