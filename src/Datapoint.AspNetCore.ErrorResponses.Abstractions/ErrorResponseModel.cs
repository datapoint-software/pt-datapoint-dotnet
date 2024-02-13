using System.Collections.Generic;

namespace Datapoint.AspNetCore.ErrorResponses
{
    /// <summary>
    /// An error response model.
    /// </summary>
    public sealed class ErrorResponseModel
    {
        /// <summary>
        /// Creates a new error response model.
        /// </summary>
        /// <param name="id">The error identifier.</param>
        /// <param name="correlationId">The error correlation identifier.</param>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        /// <param name="innerErrors">The inner errors.</param>
        /// <param name="exception">The exception.</param>
        public ErrorResponseModel(string? id, string? correlationId, string? code, string message, IReadOnlyDictionary<string, IReadOnlyCollection<ErrorModel>>? innerErrors, ExceptionModel? exception)
        {
            Id = id;
            CorrelationId = correlationId;
            Code = code;
            Message = message;
            InnerErrors = innerErrors;
            Exception = exception;
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
        /// Gets the error code.
        /// </summary>
        public string? Code { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the inner errors.
        /// </summary>
        public IReadOnlyDictionary<string, IReadOnlyCollection<ErrorModel>>? InnerErrors { get; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        public ExceptionModel? Exception { get; }
    }
}
