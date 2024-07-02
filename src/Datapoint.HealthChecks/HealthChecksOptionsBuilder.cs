using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace Datapoint.AspNetCore.HealthChecks
{
    /// <summary>
    /// An health checks options builder.
    /// </summary>
    public sealed class HealthChecksOptionsBuilder
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        internal HealthChecksOptionsBuilder(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Gets or sets the JSON serializer options.
        /// </summary>
        public JsonSerializerOptions? JsonSerializerOptions { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public PathString Path { get; set; } = new PathString("/_health");

        /// <summary>
        /// Gets or sets the stack trace enabled flag.
        /// </summary>
        public bool? StackTraceEnabled { get; set; }

        internal HealthChecksOptions BuildOptions()
        {
            return new HealthChecksOptions(
                JsonSerializerOptions
                    ?? DatapointDefaults.CreateJsonSerializerOptions(_webHostEnvironment),
                Path,
                StackTraceEnabled.HasValue
                    ? StackTraceEnabled.Value
                    : !_webHostEnvironment.IsProduction());
        }
    }
}
