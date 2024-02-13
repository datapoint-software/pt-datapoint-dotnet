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
        /// Gets or sets the stack trace enabled option.
        /// </summary>
        public bool StackTraceEnabled { get; set; } = false;

        /// <summary>
        /// Adds a JSON path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ErrorResponsesOptionsBuilder AddJsonPath(string path)

            => AddJsonPath(new PathString(path));

        /// <summary>
        /// Adds a JSON path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ErrorResponsesOptionsBuilder AddJsonPath(PathString path)
        {
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
                StackTraceEnabled);
        }
    }
}