using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Datapoint.AspNetCore.HealthChecks
{
    /// <summary>
    /// Extension methods for <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class HealthChecksWebApplicationExtensions
    {
        private const string HealthStatusOutOfRangeExceptionMessage = "Health status is not supported.";

        /// <summary>
        /// Maps the health check endpoint.
        /// </summary>
        /// <param name="app">The web application.</param>
        /// <returns>A convention routes for the health checks endpoint.</returns>
        public static IEndpointConventionBuilder MapHealthChecks(this WebApplication app) =>

            MapHealthChecks(app, null);

        /// <summary>
        /// Maps the health check endpoint.
        /// </summary>
        /// <param name="app">The web application.</param>
        /// <param name="configure">The health checks configuration action.</param>
        /// <returns>A convention routes for the health checks endpoint.</returns>
        public static IEndpointConventionBuilder MapHealthChecks(this WebApplication app, Action<HealthChecksOptionsBuilder>? configure)
        {
            var optionsBuilder = new HealthChecksOptionsBuilder(app.Environment);

            configure?.Invoke(optionsBuilder);

            var options = optionsBuilder.BuildOptions();

            return app.MapHealthChecks(options.Path, new HealthCheckOptions()
            {
                ResponseWriter = (httpContext, report) =>
                {
                    var model = new HealthCheckResponseModel(
                        report.Status.ToModel(),
                        report.Entries.ToModel(options));

                    httpContext.Response.StatusCode = report.Status switch
                    {
                        HealthStatus.Healthy => StatusCodes.Status200OK,
                        HealthStatus.Degraded => StatusCodes.Status200OK,
                        HealthStatus.Unhealthy => StatusCodes.Status503ServiceUnavailable,
                        _ => throw new NotImplementedException(HealthStatusOutOfRangeExceptionMessage)
                    };

                    httpContext.Response.WriteAsJsonAsync(
                        model,
                        options.JsonSerializerOptions,
                        httpContext.RequestAborted);

                    return Task.CompletedTask;
                }
            });
        }

        private static string ToModel(this HealthStatus healthStatus) => healthStatus switch
        {
            HealthStatus.Healthy => "pass",
            HealthStatus.Unhealthy => "fail",
            HealthStatus.Degraded => "warn",
            _ => throw new NotImplementedException(HealthStatusOutOfRangeExceptionMessage)
        };

        private static Dictionary<string, HealthCheckModel>? ToModel(this IReadOnlyDictionary<string, HealthReportEntry> entries, HealthChecksOptions options)
        {
            if (entries.Count == 0)
                return null;

            return entries.Select(kv =>
            {
                var tags = kv.Value.Tags.ToList();

                if (tags.Count == 0)
                    tags = null;

                return KeyValuePair.Create(kv.Key, new HealthCheckModel(
                    kv.Value.Status.ToModel(),
                    tags,
                    kv.Value.Description,
                    kv.Value.Duration,
                    options.StackTraceEnabled && kv.Value.Exception is not null
                        ? kv.Value.Exception.StackTrace
                        : null));
            })
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
