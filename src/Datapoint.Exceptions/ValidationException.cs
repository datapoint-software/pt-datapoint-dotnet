using System;

namespace Datapoint.AspNetCore
{
    /// <summary>
    /// An exception thrown on object validation failures.
    /// </summary>
    public class ValidationException : Exception
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

