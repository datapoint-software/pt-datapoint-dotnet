using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Datapoint.AspNetCore.ErrorResponses
{
    /// <summary>
    /// An error responses options builder.
    /// </summary>
    public sealed class ErrorResponsesOptionsBuilder
    {
        private readonly List<PathString> _jsonPaths = new()
        {
            { new("/api") }
        };

        /// <summary>
        /// Gets or sets the error message factory.
        /// </summary>
        public Func<Exception, string?>? ErrorMessageFactory { get; set; }

        /// <summary>
        /// Gets or sets the JSON serializer options.
        /// </summary>
        public JsonSerializerOptions? JsonSerializerOptions { get; set; }

        /// <summary>
        /// Gets or sets the error source.
        /// </summary>
        public string Source { get; set; } = "app";

        /// <summary>
        /// Gets or sets the stack trace enabled option.
        /// </summary>
        public bool StackTraceEnabled { get; set; } = false;

        /// <summary>
        /// Adds a JSON path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The error response options builder.</returns>
        public ErrorResponsesOptionsBuilder AddJsonPath(string path) =>

            AddJsonPath(new PathString(path));

        /// <summary>
        /// Adds a JSON path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The error response options builder.</returns>
        public ErrorResponsesOptionsBuilder AddJsonPath(PathString path)
        {
            _jsonPaths.Add(path);

            return this;
        }

        /// <summary>
        /// Clears the JSON paths.
        /// </summary>
        /// <returns>The error responses options builder.</returns>
        public ErrorResponsesOptionsBuilder ClearJsonPaths()
        {
            _jsonPaths.Clear();

            return this;
        }

        /// <summary>
        /// Sets the JSON path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The error responses options builder.</returns>
        public ErrorResponsesOptionsBuilder SetJsonPath(string path) =>

            SetJsonPath(new PathString(path));


        /// <summary>
        /// Sets the JSON path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The error responses options builder.</returns>
        public ErrorResponsesOptionsBuilder SetJsonPath(PathString path)
        {
            _jsonPaths.Clear();
            _jsonPaths.Add(path);

            return this;
        }

        internal ErrorResponsesOptions BuildOptions()
        {
            return new ErrorResponsesOptions(
                ErrorMessageFactory,
                _jsonPaths,
                JsonSerializerOptions ?? new JsonSerializerOptions()
                {
                    AllowTrailingCommas = false,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                    IgnoreReadOnlyFields = false,
                    IgnoreReadOnlyProperties = false,
                    ReadCommentHandling = JsonCommentHandling.Disallow,
                    WriteIndented = false
                },
                Source,
                StackTraceEnabled);
        }
    }
}