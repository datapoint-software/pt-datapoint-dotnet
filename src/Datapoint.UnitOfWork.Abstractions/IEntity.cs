using System;

namespace Datapoint.UnitOfWork
{
    /// <summary>
    /// An entity.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// An entity with an identifier.
        /// </summary>
        public interface IId : IEntity
        {
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            long Id { get; set; }
        }

        /// <summary>
        /// An entity with a public identifier.
        /// </summary>
        public interface IPublicId : IEntity
        {
            /// <summary>
            /// Gets or sets the public identifier.
            /// </summary>
            Guid PublicId { get; set; }
        }

        /// <summary>
        /// An entity with a row version identifier.
        /// </summary>
        public interface IRowVersionId : IEntity
        {
            /// <summary>
            /// Gets or sets the row version identifier.
            /// </summary>
            Guid RowVersionId { get; set; }
        }
    }
}
