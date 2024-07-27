using System;
using System.Runtime.Serialization;

namespace Datapoint.AspNetCore
{
    /// <summary>
    /// An exception thrown on authentication failures.
    /// </summary>
    public class AuthenticationException : Exception
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
