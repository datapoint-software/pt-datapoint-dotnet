using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    /// <summary>
    /// An Entity Framework Core unit of work.
    /// </summary>
    public abstract class EntityFrameworkCoreUnitOfWork : DbContext, IUnitOfWork
    {
        /// <summary>
        /// Creates a new Entity Framework Core unit of work.
        /// </summary>
        /// <param name="options">The context options.</param>
        protected EntityFrameworkCoreUnitOfWork(DbContextOptions options) : base(options)
        {
            ChangeTracker.Tracked += (sender, e) =>
            {
                if (e.Entry.State == EntityState.Added)
                {
                    if (e.Entry.Entity is IEntity entity)
                    {
                        if (entity.PublicId == default)
                            entity.PublicId = Guid.NewGuid();

                        if (entity.RowVersionId == default)
                            entity.RowVersionId = Guid.NewGuid();
                    }
                }
            };

            SavingChanges += (sender, e) =>
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    if (entry.State == EntityState.Modified)
                    {
                        if (entry.Entity is IEntity entity)
                        {
                            if (entity.PublicId == default)
                                entity.PublicId = Guid.NewGuid();

                            if (entity.RowVersionId == default || entity.RowVersionId == entry.OriginalValues.GetValue<Guid>("RowVersionId"))
                                entity.RowVersionId = Guid.NewGuid();
                        }
                    }
                }
            };

            ChangeTracker.LazyLoadingEnabled = false;
        }

        /// <inheritdoc />
        Task IUnitOfWork.SaveChangesAsync(CancellationToken ct) =>

            base.SaveChangesAsync(ct);
    }
}
