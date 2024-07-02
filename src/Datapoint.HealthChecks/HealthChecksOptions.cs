using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Datapoint.AspNetCore.HealthChecks
{
    internal sealed class HealthChecksOptions
    {
        internal HealthChecksOptions(JsonSerializerOptions jsonSerializerOptions, PathString path, bool stackTraceEnabled)
        {
            JsonSerializerOptions = jsonSerializerOptions;
            Path = path;
            StackTraceEnabled = stackTraceEnabled;
        }

        internal JsonSerializerOptions JsonSerializerOptions { get; }

        internal PathString Path { get; }

        internal bool StackTraceEnabled { get; }
    }
}
