using System;

namespace Datapoint
{
    /// <summary>
    /// A concurrency exception.
    /// </summary>
    public sealed class ConcurrencyException : Exception
    {
        /// <inheritdoc />
        public ConcurrencyException()
        {
        }

        /// <inheritdoc />
        public ConcurrencyException(string? message) : base(message)
        {
        }

        /// <inheritdoc />
        public ConcurrencyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
