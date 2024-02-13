using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    /// <summary>
    /// Entity type builder extensions.
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// Maps the default entity properties.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity type builder.</param>
        /// <returns>The entity type builder.</returns>
        public static EntityTypeBuilder<TEntity> Entity<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : class, IEntity
        {
            entity.HasKey(e => e.Id);
            entity.HasAlternateKey(e => e.PublicId);

            entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(e => e.PublicId).IsRequired();
            entity.Property(e => e.RowVersionId).IsRequired().IsConcurrencyToken();

            return entity;
        }
    }
}
