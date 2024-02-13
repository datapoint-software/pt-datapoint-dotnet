using System;

namespace Datapoint
{
    /// <summary>
    /// An authentication exception.
    /// </summary>
    public sealed class AuthenticationException : Exception
    {
        /// <inheritdoc />
        public AuthenticationException()
        {
        }

        /// <inheritdoc />
        public AuthenticationException(string? message) : base(message)
        {
        }

        /// <inheritdoc />
        public AuthenticationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
