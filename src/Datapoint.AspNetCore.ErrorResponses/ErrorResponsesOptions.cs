using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Datapoint.AspNetCore.ErrorResponses
{
    internal sealed class ErrorResponsesOptions
    {
        public ErrorResponsesOptions(
            Func<Exception, string?>? errorMessageFactory,
            IReadOnlyCollection<PathString> jsonPaths,
            JsonSerializerOptions jsonSerializerOptions,
            string source,
            bool stackTraceEnabled)
        {
            ErrorMessageFactory = errorMessageFactory;
            JsonPaths = jsonPaths;
            JsonSerializerOptions = jsonSerializerOptions;
            Source = source;
            StackTraceEnabled = stackTraceEnabled;
        }

        public Func<Exception, string?>? ErrorMessageFactory { get; }

        public IReadOnlyCollection<PathString> JsonPaths { get; }

        public JsonSerializerOptions JsonSerializerOptions { get; }

        public string Source { get; }

        public bool StackTraceEnabled { get; }
    }
}
