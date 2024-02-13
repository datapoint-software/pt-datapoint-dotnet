using System;

namespace Datapoint
{
    /// <summary>
    /// An authorization exception.
    /// </summary>
    public sealed class AuthorizationException : Exception
    {
        /// <inheritdoc />
        public AuthorizationException()
        {
        }

        /// <inheritdoc />
        public AuthorizationException(string? message) : base(message)
        {
        }

        /// <inheritdoc />
        public AuthorizationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
