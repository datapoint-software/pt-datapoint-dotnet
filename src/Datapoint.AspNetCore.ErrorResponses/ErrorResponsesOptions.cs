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
            bool stackTraceEnabled)
        {
            ErrorMessageFactory = errorMessageFactory;
            JsonPaths = jsonPaths;
            JsonSerializerOptions = jsonSerializerOptions;
            StackTraceEnabled = stackTraceEnabled;
        }

        public Func<Exception, string?>? ErrorMessageFactory { get; }

        public IReadOnlyCollection<PathString> JsonPaths { get; }

        public JsonSerializerOptions JsonSerializerOptions { get; }

        public bool StackTraceEnabled { get; set; }
    }
}
