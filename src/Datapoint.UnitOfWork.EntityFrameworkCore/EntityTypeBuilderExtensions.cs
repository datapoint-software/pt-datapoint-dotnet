using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    /// <summary>
    /// Entity type builder extensions.
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// Maps the identifier property.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>The key builder.</returns>
        public static EntityTypeBuilder<TEntity> Id<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : class, IEntity.IId
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();

            return entity;
        }

        /// <summary>
        /// Maps the public identifier property.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>The key builder.</returns>
        public static EntityTypeBuilder<TEntity> PublicId<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : class, IEntity.IPublicId
        {
            entity.HasAlternateKey(e => e.PublicId);

            entity.Property(e => e.PublicId).IsRequired();

            return entity;
        }

        /// <summary>
        /// Maps the row version identifier property.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>The key builder.</returns>
        public static EntityTypeBuilder<TEntity> RowVersionId<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : class, IEntity.IRowVersionId
        {
            entity.Property(e => e.RowVersionId).IsConcurrencyToken().IsRequired();

            return entity;
        }
    }
}
