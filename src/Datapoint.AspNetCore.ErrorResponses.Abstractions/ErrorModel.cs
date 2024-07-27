namespace Datapoint.AspNetCore.ErrorResponses
{
    /// <summary>
    /// An error model.
    /// </summary>
    public sealed class ErrorModel
    {
        /// <summary>
        /// Creates an error model.
        /// </summary>
        /// <param name="name">The error name.</param>
        /// <param name="propertyName">The error property name.</param>
        /// <param name="message">The error message.</param>
        public ErrorModel(string name, string? propertyName, string? message)
        {
            Name = name;
            PropertyName = propertyName;
            Message = message;
        }

        /// <summary>
        /// Gets the error name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the error property name.
        /// </summary>
        public string? PropertyName { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string? Message { get; }
    }
}
