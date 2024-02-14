using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datapoint.UnitOfWork.EntityFrameworkCore
{
    /// <summary>
    /// Entity type builder extensions.
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// Maps all entity properties.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="builder">The entity type builder.</param>
        /// <returns>The key builder.</returns>
        public static EntityTypeBuilder<TEntity> Entity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IEntity
        {
            builder.Id();
            builder.PublicId();
            builder.RowVersionId();

            return builder;
        }

        /// <summary>
        /// Maps the identifier property.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="builder">The entity type builder.</param>
        /// <returns>The key builder.</returns>
        public static EntityTypeBuilder<TEntity> Id<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IEntity
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();

            return builder;
        }

        /// <summary>
        /// Maps the public identifier property.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="builder">The entity type builder.</param>
        /// <returns>The key builder.</returns>
        public static EntityTypeBuilder<TEntity> PublicId<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IEntity
        {
            builder.HasAlternateKey(e => e.PublicId);

            builder.Property(e => e.PublicId).IsRequired();

            return builder;
        }

        /// <summary>
        /// Maps the row version identifier property.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="builder">The entity type builder.</param>
        /// <returns>The key builder.</returns>
        public static EntityTypeBuilder<TEntity> RowVersionId<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IEntity
        {
            builder.Property(e => e.RowVersionId).IsConcurrencyToken().IsRequired();

            return builder;
        }
    }
}
