using System.Collections.Generic;

namespace Datapoint.AspNetCore.ErrorResponses
{
    /// <summary>
    /// An error.
    /// </summary>
    public sealed class ErrorModel
    {
        /// <summary>
        /// Creates an error model.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        public ErrorModel(string? code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Gets the code.
        /// </summary>
        public string? Code { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; }
    }
}
