using System.Collections.Generic;

namespace Datapoint.AspNetCore.ErrorResponses
{
    /// <summary>
    /// An error model.
    /// </summary>
    public sealed class ErrorResponseModel
    {
        /// <summary>
        /// Creates an error model.
        /// </summary>
        /// <param name="id">The error identifier.</param>
        /// <param name="correlationId">The error correlation identifier.</param>
        /// <param name="name">The error name.</param>
        /// <param name="message">The error message.</param>
        /// <param name="innerErrors">The error inner errors.</param>
        /// <param name="stackTrace">The error stack trace.</param>
        public ErrorResponseModel(string? id, string? correlationId, string? name, string message, IEnumerable<ErrorModel>? innerErrors, string? stackTrace)
        {
            Id = id;
            CorrelationId = correlationId;
            Name = name;
            Message = message;
            InnerErrors = innerErrors;
            StackTrace = stackTrace;
        }

        /// <summary>
        /// Gets the error identifier.
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// Gets the error correlation identifier.
        /// </summary>
        public string? CorrelationId { get; }

        /// <summary>
        /// Gets the error name.
        /// </summary>
        public string? Name { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the inner errors.
        /// </summary>
        public IEnumerable<ErrorModel>? InnerErrors { get; }

        /// <summary>
        /// Gets the error stack trace.
        /// </summary>
        public string? StackTrace { get; }
    }
}
