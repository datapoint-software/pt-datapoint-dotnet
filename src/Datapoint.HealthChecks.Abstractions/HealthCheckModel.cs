using System;
using System.Collections.Generic;

namespace Datapoint.AspNetCore.HealthChecks
{
    /// <summary>
    /// An health check model.
    /// </summary>
    public sealed class HealthCheckModel
    {
        /// <summary>
        /// Creates an health check model.
        /// </summary>
        /// <param name="status">The health check status.</param>
        /// <param name="tags">The health check tags.</param>
        /// <param name="message">The health check message.</param>
        /// <param name="duration">The health check duration.</param>
        /// <param name="stackTrace">The stack trace.</param>
        public HealthCheckModel(string status, IReadOnlyCollection<string>? tags, string? message, TimeSpan duration, string? stackTrace)
        {
            Status = status;
            Tags = tags;
            Message = message;
            Duration = duration;
            StackTrace = stackTrace;
        }


        /// <summary>
        /// Gets the status.
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        public IReadOnlyCollection<string>? Tags { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string? Message { get; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        public TimeSpan Duration { get; }

        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        public string? StackTrace { get; }
    }
}
