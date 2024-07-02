using System.Collections.Generic;

namespace Datapoint.AspNetCore.HealthChecks
{
    /// <summary>
    /// An health check response model.
    /// </summary>
    public sealed class HealthCheckResponseModel
    {
        /// <summary>
        /// Creates an health check response model.
        /// </summary>
        /// <param name="status">The health check status.</param>
        /// <param name="entries">The health check entries.</param>
        public HealthCheckResponseModel(string status, IReadOnlyDictionary<string, HealthCheckModel>? entries)
        {
            Status = status;
            Entries = entries;
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// Gets the entries.
        /// </summary>
        public IReadOnlyDictionary<string, HealthCheckModel>? Entries { get; }
    }
}
