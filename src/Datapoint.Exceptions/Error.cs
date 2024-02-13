namespace Datapoint
{
    /// <summary>
    /// An exception error information.
    /// </summary>
    public struct Error
    {
        /// <summary>
        /// Creates a new exception error information.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        public Error(string? code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Gets the code.
        /// </summary>
        public string? Code { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; }
    }
}
