﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

#pragma warning disable CA1859

namespace Datapoint.AspNetCore.ErrorResponses
{
    /// <summary>
    /// Extension methods for <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class ErrorResponsesApplicationBuilderExtensions
    {
        /// <summary>
        /// Enables error responses.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseErrorResponses(this IApplicationBuilder app)

            => app.UseErrorResponses(null);

        /// <summary>
        /// Enables error responses.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configure">The configuration action.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseErrorResponses(this IApplicationBuilder app, Action<ErrorResponsesOptionsBuilder>? configure)
        {
            var optionsBuilder = new ErrorResponsesOptionsBuilder();

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
                    httpContext.Response.Clear();

                    var ct = httpContext.RequestAborted;

                    var json = options.JsonPaths.Any(jp => 
                        jp.HasValue == false || 
                        jp.Value.Equals("/", StringComparison.OrdinalIgnoreCase) || 
                        httpContext.Request.Path.StartsWithSegments(jp));

                    if (json)
                    {
                        await WriteJsonResponseAsync(
                            httpContext,
                            exception,
                            options.StackTraceEnabled,
                            options.ErrorMessageFactory,
                            options.JsonSerializerOptions,
                            ct);
                    }
                    else
                    {
                        WriteRedirectResponse(
                            httpContext,
                            exception,
                            options.StackTraceEnabled,
                            options.ErrorMessageFactory);
                    }
                }
            });
        }

        private static Task WriteJsonResponseAsync(HttpContext httpContext, Exception exception, bool includeStackTrace, Func<Exception, string?>? errorMessageFactory, JsonSerializerOptions jsonSerializerOptions, CancellationToken ct)
        {
            httpContext.Response.StatusCode = CreateStatusCode(exception);

            return httpContext.Response.WriteAsJsonAsync(
                new ErrorResponseModel(
                    exception.TryGetId(out var id) ? id : null,
                    exception.TryGetCorrelationId(out var correlationId) ? correlationId : null,
                    exception.TryGetErrorCode(out var errorCode) ? errorCode : null,
                    CreateErrorMessage(exception, errorMessageFactory),
                    exception.TryGetInnerErrors(out var innerErrors) 
                        ? innerErrors.ToDictionary(
                            kv => kv.Key,
                            kv => (IReadOnlyCollection<ErrorModel>) kv.Value.Select(
                                ie => new ErrorModel(
                                    ie.Code,
                                    ie.Message))
                                .ToArray())
                        : null,
                    includeStackTrace ? CreateExceptionModel(exception) : null),
                jsonSerializerOptions,
                ct);
        }            

        private static void WriteRedirectResponse(HttpContext httpContext, Exception exception, bool includeStackTrace, Func<Exception, string?>? errorMessageFactory)
        {
            var sb = new StringBuilder("/error");
            var sbInitialLength = sb.Length;

            if (exception.TryGetId(out var id))
            {
                sb.Append("?id=");
                sb.Append(id);
            }

            sb.Append(sb.Length > sbInitialLength ? '&' : '?');
            sb.Append("correlationId=");
            if (exception.TryGetCorrelationId(out var correlationId))
                sb.Append(correlationId);
            else
                sb.Append(httpContext.TraceIdentifier);

            if (exception.TryGetErrorCode(out var errorCode))
            {
                sb.Append("&code=");
                sb.Append(Encode(errorCode));
            }

            sb.Append("&message=");
            sb.Append(Encode(CreateErrorMessage(exception, errorMessageFactory)));

            sb.Append("&statusCode=");
            sb.Append(CreateStatusCode(exception));

            if (includeStackTrace && !string.IsNullOrEmpty(exception.StackTrace))
            {
                sb.Append("&stackTrace=");
                sb.Append(Encode(
                    (exception.GetType().FullName ?? exception.GetType().Name) + ": \n" +
                    exception.StackTrace));
            }

            httpContext.Response.Redirect(sb.ToString());
        }

        private static string CreateErrorMessage(Exception exception, Func<Exception, string?>? errorMessageFactory) =>

            exception.TryGetErrorMessage(out var errorMessage)
                ? errorMessage
                : errorMessageFactory?.Invoke(exception) ?? (
                    exception is AuthenticationException ? "This operation could not be peformed because your session has expired." :
                    exception is AuthorizationException ? "This operation could not be performed because you have insufficient privileges." :
                    exception is ValidationException ? "This operation could not be performed because some input fields are not valid." :
                        "This operation could not be performed due to an unexpected error.");

        private static ExceptionModel CreateExceptionModel(Exception exception)
        {
            var et = exception.GetType();

            return new ExceptionModel(
                et.Name,
                et.FullName,
                exception.Message,
                exception.Source,
                exception.StackTrace,
                exception.InnerException is null ? null :
                    CreateExceptionModel(exception.InnerException));
        }

        private static int CreateStatusCode(Exception exception) =>

            exception is AuthenticationException ? 401 :
            exception is AuthorizationException ? 403 :
            exception is BusinessException or ConcurrencyException ? 409 :
            exception is TimeoutException ? 504 :
            exception is ValidationException ? 400 :
                500;

        private static string Encode(string component) => Convert.ToBase64String(
            Encoding.Default.GetBytes(
                HttpUtility.UrlEncode(
                    component)));
    }
}
