using System;

namespace Datapoint.UnitOfWork
{
    /// <summary>
    /// An entity.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// Gets or sets the public identifier.
        /// </summary>
        Guid PublicId { get; set; }

        /// <summary>
        /// Gets or sets the row version identifier.
        /// </summary>
        Guid RowVersionId { get; set; }
    }
}
