namespace Datapoint.AspNetCore.ErrorResponses
{
    /// <summary>
    /// An exception model.
    /// </summary>
    public sealed class ExceptionModel
    {
        /// <summary>
        /// Creates an exception model.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="message">The message.</param>
        /// <param name="source">The source.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <param name="innerException">The inner exception.</param>
        public ExceptionModel(string name, string? fullName, string message, string? source, string? stackTrace, ExceptionModel? innerException)
        {
            Name = name;
            FullName = fullName;
            Message = message;
            Source = source;
            StackTrace = stackTrace;
            InnerException = innerException;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        public string? FullName { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the source.
        /// </summary>
        public string? Source { get; }

        /// <summary>
        /// Gets the stack trace.
        /// </summary>
        public string? StackTrace { get; }

        /// <summary>
        /// Gets the inner exception.
        /// </summary>
        public ExceptionModel? InnerException { get; }
    }
}
