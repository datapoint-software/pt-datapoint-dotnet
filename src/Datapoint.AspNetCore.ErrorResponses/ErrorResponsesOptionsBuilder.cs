using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Datapoint.AspNetCore.ErrorResponses
{
    /// <summary>
    /// An error responses options builder.
    /// </summary>
    public sealed class ErrorResponsesOptionsBuilder
    {
        /// <summary>
        /// Gets or sets the middleware error path.
        /// </summary>
        public PathString ErrorPath { get; set; } = new PathString("/error");

        /// <summary>
        /// Gets the middleware JSON paths.
        /// </summary>
        public List<PathString> JsonPaths { get; } = new()
        {
            { new PathString("/api") }
        };

        /// <summary>
        /// Gets or sets the middleware error message factory.
        /// </summary>
        public Func<Exception, string?>? ErrorMessageFactory { get; set; }

        /// <summary>
        /// Gets or sets the middleware JSON serializer options.
        /// </summary>
        public JsonSerializerOptions JsonSerializerOptions { get; set; } = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        /// <summary>
        /// Gets or sets the middleware stack trace enabled flag.
        /// </summary>
        public bool StackTraceEnabled { get; set; } = false;

        internal ErrorResponsesOptions BuildOptions() =>

            new ErrorResponsesOptions(
                ErrorPath,
                ErrorMessageFactory,
                JsonPaths,
                JsonSerializerOptions,
                StackTraceEnabled);
    }
}
