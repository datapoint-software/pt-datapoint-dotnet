using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;

namespace Datapoint.AspNetCore
{
    /// <summary>
    /// Datapoints defaults.
    /// </summary>
    public static class DatapointDefaults
    {
        /// <summary>
        /// Configures the given JSON serializer options with the default values.
        /// </summary>
        /// <param name="jsonSerializerOptions">The JSON serializer options.</param>
        /// <returns></returns>
        public static JsonSerializerOptions WithDefaults(this JsonSerializerOptions jsonSerializerOptions) =>

            WithDefaults(jsonSerializerOptions, null);

        /// <summary>
        /// Configures the given JSON serializer options with the default values.
        /// </summary>
        /// <param name="jsonSerializerOptions">The JSON serializer options.</param>
        /// <param name="webHostEnvironment">The Web host environment.</param>
        /// <returns></returns>
        public static JsonSerializerOptions WithDefaults(this JsonSerializerOptions jsonSerializerOptions, IWebHostEnvironment? webHostEnvironment)
        {
            if (jsonSerializerOptions.IsReadOnly)
                throw new ArgumentException("Options are read only and can not be modified.");

            jsonSerializerOptions.AllowTrailingCommas = false;
            jsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            jsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            jsonSerializerOptions.IncludeFields = false;
            jsonSerializerOptions.IgnoreReadOnlyFields = true;
            jsonSerializerOptions.IgnoreReadOnlyProperties = false;
            jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Disallow;
            jsonSerializerOptions.WriteIndented = webHostEnvironment?.IsDevelopment() ?? false;

            return jsonSerializerOptions;
        }

        /// <summary>
        /// Builds the default JSON serializer options.
        /// </summary>
        /// <returns>The JSON serializer options.</returns>
        public static JsonSerializerOptions CreateJsonSerializerOptions() =>

            CreateJsonSerializerOptions(null);

        /// <summary>
        /// Builds the default JSON serializer options.
        /// </summary>
        /// <param name="webHostEnvironment">The web host environment.</param>
        /// <returns>The JSON serializer options.</returns>
        public static JsonSerializerOptions CreateJsonSerializerOptions(IWebHostEnvironment? webHostEnvironment)
        {
            return new JsonSerializerOptions()
                .WithDefaults(webHostEnvironment);
        }
    }
}
