using System;

namespace Datapoint
{
    /// <summary>
    /// A business exception.
    /// </summary>
    public sealed class BusinessException : Exception
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
