namespace Datapoint.AspNetCore
{
    /// <summary>
    /// An error.
    /// </summary>
    public sealed class Error
    {
        /// <summary>
        /// Creates a validation error.
        /// </summary>
        /// <param name="name">The validation error name.</param>
        /// <param name="propertyName">The validation error property name.</param>
        /// <param name="message">The validation error message.</param>
        public Error(string name, string? propertyName, string? message)
        {
            Name = name;
            PropertyName = propertyName;
            Message = message;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the property name.
        /// </summary>
        public string? PropertyName { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string? Message { get; }
    }
}