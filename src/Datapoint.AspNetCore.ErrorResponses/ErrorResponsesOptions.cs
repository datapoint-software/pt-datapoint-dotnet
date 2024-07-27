using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Datapoint.AspNetCore.ErrorResponses
{
    internal sealed class ErrorResponsesOptions
    {
        public ErrorResponsesOptions(PathString errorPath, Func<Exception, string?>? errorMessageFactory, IEnumerable<PathString> jsonPaths, JsonSerializerOptions jsonSerializerOptions, bool stackTraceEnabled)
        {
            ErrorPath = errorPath;
            ErrorMessageFactory = errorMessageFactory;
            JsonPaths = jsonPaths;
            JsonSerializerOptions = jsonSerializerOptions;
            StackTraceEnabled = stackTraceEnabled;
        }

        internal PathString ErrorPath { get; }

        internal Func<Exception, string?>? ErrorMessageFactory { get; }

        internal IEnumerable<PathString> JsonPaths { get; }

        internal JsonSerializerOptions JsonSerializerOptions { get; }

        internal bool StackTraceEnabled { get; }
    }
}
