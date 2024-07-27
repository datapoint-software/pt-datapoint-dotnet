using System;
using System.Runtime.Serialization;

namespace Datapoint.AspNetCore
{
    /// <summary>
    /// An exception thrown on business criteria validation failure.
    /// </summary>
    public class BusinessException : Exception
    {
        /// <inheritdoc />
        public BusinessException()
        {
        }

        /// <inheritdoc />
        public BusinessException(string? message) : base(message)
        {
        }

        /// <inheritdoc />
        public BusinessException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
