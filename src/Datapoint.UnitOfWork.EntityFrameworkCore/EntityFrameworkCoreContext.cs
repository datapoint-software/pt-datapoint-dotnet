using Microsoft.EntityFrameworkCore;
using System;

namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    /// <summary>
    /// An Entity Framework Core context.
    /// </summary>
    public abstract class EntityFrameworkCoreContext : DbContext
    {
        /// <inheritdoc />
        protected EntityFrameworkCoreContext(DbContextOptions options) : base(options)
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

                            else if (entity.RowVersionId == default || entity.RowVersionId == entry.OriginalValues.GetValue<Guid>("RowVersionId"))
                                entity.RowVersionId = Guid.NewGuid();
                        }
                    }
                }
            };

            ChangeTracker.LazyLoadingEnabled = false;
        }
    }
}
