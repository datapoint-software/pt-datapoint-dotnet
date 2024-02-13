using System;

namespace Datapoint.UnitOfWork
{
    /// <summary>
    /// An entity.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// Gets or sets the entity globally unique public identifier.
        /// </summary>
        Guid PublicId { get; set; }

        /// <summary>
        /// Gets or sets the entity globally unique row version identifier.
        /// </summary>
        Guid RowVersionId { get; set; }
    }
}
