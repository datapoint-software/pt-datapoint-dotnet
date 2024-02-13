using System;

namespace Datapoint
{
    /// <summary>
    /// A validation exception.
    /// </summary>
    public sealed class ValidationException : Exception
    {
        /// <inheritdoc />
        public ValidationException()
        {
        }

        /// <inheritdoc />
        public ValidationException(string? message) : base(message)
        {
        }

        /// <inheritdoc />
        public ValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
