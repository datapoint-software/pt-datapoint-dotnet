using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Datapoint.AspNetCore.ErrorResponses
{
    /// <summary>
    /// Extension methods for <see cref="IApplicationBuilder" />.
    /// </summary>
    public static class ErrorResponsesApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds an error responses middleware to the application execution pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseErrorResponses(this IApplicationBuilder app) =>

            UseErrorResponses(app, null);

        /// <summary>
        /// Adds an error responses middleware to the application execution pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configure">The middleware configuration action.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseErrorResponses(this IApplicationBuilder app, Action<ErrorResponsesOptionsBuilder>? configure)
        {
            var webHostEnvironment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            var optionsBuilder = new ErrorResponsesOptionsBuilder();

            if (webHostEnvironment.IsDevelopment())
                optionsBuilder.StackTraceEnabled = true;

            configure?.Invoke(optionsBuilder);

            var options = optionsBuilder.BuildOptions();

            return app.Use(async (httpContext, next) =>
            {
                try
                {
                    await next(httpContext);
                }
                catch (Exception exception)
                {
                    var ct = httpContext.RequestAborted;

                    var message = exception is BusinessException or ValidationException
                            ? exception.Message
                            : options.ErrorMessageFactory?.Invoke(exception)
                                ?? "We have encountered an unexpected error.";

                    var statusCode =
                        exception is AuthenticationException ? 401 :
                        exception is AuthorizationException ? 403 :
                        exception is BusinessException ? 409 :
                        exception is ValidationException ? 400 :
                            500;

                    exception.TryGetCorrelationId(out var correlationId);
                    exception.TryGetName(out var name);
                    exception.TryGetId(out var id);

                    foreach (var jsonPath in options.JsonPaths)
                    {
                        if (jsonPath.HasValue && httpContext.Request.Path.StartsWithSegments(jsonPath))
                        {
                            await WriteJsonErrorResponseAsync(
                                httpContext,
                                exception,
                                options.JsonSerializerOptions,
                                options.StackTraceEnabled,
                                id,
                                correlationId,
                                name,
                                message,
                                statusCode,
                                ct);

                            return;
                        }
                    }

                    httpContext.Response.Redirect(
                        BuildRedirectLocation(
                            httpContext,
                            exception,
                            options.ErrorPath,
                            options.StackTraceEnabled,
                            id,
                            correlationId,
                            name,
                            message,
                            statusCode));
                }
            });
        }

        private static string BuildRedirectLocation(HttpContext httpContext, Exception exception, PathString errorPath, bool stackTraceEnabled, string? id, string? correlationId, string? name, string message, int statusCode)
        {
            var sb = new StringBuilder(errorPath);
            var sbBaseLength = sb.Length;

            if (!string.IsNullOrEmpty(id))
                sb.Append("?id=").Append(HttpUtility.UrlEncode(id));

            if (!string.IsNullOrEmpty(correlationId))
                sb.Append(sb.Length == sbBaseLength ? '?' : '&')
                    .Append("correlationId=")
                    .Append(HttpUtility.UrlEncode(correlationId));

            else if (!string.IsNullOrEmpty(httpContext.TraceIdentifier))
                sb.Append(sb.Length == sbBaseLength ? '?' : '&')
                    .Append("correlationId=")
                    .Append(HttpUtility.UrlEncode(httpContext.TraceIdentifier));

            if (!string.IsNullOrEmpty(name))
                sb.Append(sb.Length == sbBaseLength ? '?' : '&')
                    .Append("name=")
                    .Append(HttpUtility.UrlEncode(name));

            sb.Append(sb.Length == sbBaseLength ? '?' : '&')
                .Append("message=")
                .Append(HttpUtility.UrlEncode(message));

            sb.Append("&status=")
                .Append(statusCode);

            if (stackTraceEnabled && !string.IsNullOrEmpty(exception.StackTrace))
                sb.Append("&status=")
                    .Append(HttpUtility.UrlEncode(exception.StackTrace));

            return sb.ToString();
        }

        private static async Task WriteJsonErrorResponseAsync(HttpContext httpContext, Exception exception, JsonSerializerOptions jsonSerializerOptions, bool stackTraceEnabled, string? id, string? correlationId, string? name, string message, int statusCode, CancellationToken ct)
        {
            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(
                new ErrorResponseModel(
                    id,
                    correlationId,
                    name,
                    message,
                    exception.TryGetInnerErrors(out var innerErrors)
                        ? innerErrors.Select(e => new ErrorModel(
                            e.Name,
                            e.PropertyName,
                            e.Message))
                        : null,
                    stackTraceEnabled
                        ? exception.StackTrace
                        : null),
                jsonSerializerOptions,
                ct);
        }
    }
}
