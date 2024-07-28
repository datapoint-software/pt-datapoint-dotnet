using System;
using System.Runtime.Serialization;

namespace Datapoint
{
    /// <summary>
    /// An exception thrown on authorization failures.
    /// </summary>
    public class AuthorizationException : Exception
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
