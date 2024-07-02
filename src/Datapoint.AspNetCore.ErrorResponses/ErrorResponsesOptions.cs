using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Datapoint.AspNetCore.ErrorResponses
{
    internal sealed class ErrorResponsesOptions
    {
        internal ErrorResponsesOptions(
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

        internal Func<Exception, string?>? ErrorMessageFactory { get; }

        internal IReadOnlyCollection<PathString> JsonPaths { get; }

        internal JsonSerializerOptions JsonSerializerOptions { get; }

        internal string Source { get; }

        internal bool StackTraceEnabled { get; }
    }
}
