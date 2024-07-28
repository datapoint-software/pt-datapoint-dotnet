using System;
using System.Collections.Generic;

namespace Datapoint
{
    /// <summary>
    /// Helper methods for <see cref="Exception" />.
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Adds a value to the exception data set.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>The exception.</returns>
        public static TException Add<TException>(this TException exception, string key, object value) where TException : Exception
        {
            exception.Data.Add(key, value);
            return exception;
        }

        /// <summary>
        /// Adds the correlation identifier to the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <returns>The exception.</returns>
        public static TException AddCorrelationId<TException>(this TException exception, Guid correlationId) where TException : Exception =>

            exception.AddCorrelationId(correlationId.ToString());

        /// <summary>
        /// Adds the correlation identifier to the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <returns>The exception.</returns>
        public static TException AddCorrelationId<TException>(this TException exception, string correlationId) where TException : Exception =>

            exception.Add("CorrelationId", correlationId);

        /// <summary>
        /// Adds an inner error.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="error">The error.</param>
        /// <returns>The exception.</returns>
        public static TException AddInnerError<TException>(this TException exception, Error error) where TException : Exception
        {
            var innerErrors = exception.Data.Contains("InnerErrors")
                ? (List<Error>)exception.Data["InnerErrors"]!
                : (List<Error>)(exception.Data["InnerErrors"] = new List<Error>(1));

            innerErrors.Add(error);

            return exception;
        }

        /// <summary>
        /// Adds the identifier to the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>The exception.</returns>
        public static TException AddId<TException>(this TException exception, Guid id) where TException : Exception =>

            exception.AddId(id.ToString());

        /// <summary>
        /// Adds the identifier to the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>The exception.</returns>
        public static TException AddId<TException>(this TException exception, string id) where TException : Exception =>

            exception.Add("Id", id);

        /// <summary>
        /// Adds the name to the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="name">The name.</param>
        /// <returns>The exception.</returns>
        public static TException AddName<TException>(this TException exception, string name) where TException : Exception =>

            exception.Add("Name", name);

        /// <summary>
        /// Attempts to get and cast the identifier from the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="id">The exception identifier output variable.</param>
        /// <returns>The result.</returns>
        public static bool TryGetId<TException>(this TException exception, out string id) where TException : Exception =>

            exception.TryGetValue("Id", out id);

        /// <summary>
        /// Attempts to get and cast the inner errors from the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="innerErrors">The exception inner errors output variable.</param>
        /// <returns>The exception.</returns>
        public static bool TryGetInnerErrors<TException>(this TException exception, out IEnumerable<Error> innerErrors) where TException : Exception
        {
            if (exception.TryGetValue<TException, List<Error>>("InnerErrors", out var innerErrorsList))
            {
                innerErrors = innerErrorsList!;
                return true;
            }
            else
            {
                innerErrors = default!;
                return false;
            }
        }

        /// <summary>
        /// Attempts to get and cast the correlation identifier from the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="correlationId">The exception correlation identifier output variable.</param>
        /// <returns>The result.</returns>
        public static bool TryGetCorrelationId<TException>(this TException exception, out string correlationId) where TException : Exception =>

            exception.TryGetValue("CorrelationId", out correlationId);

        /// <summary>
        /// Attempts to get and cast the name from the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="name">The exception name output variable.</param>
        /// <returns>The result.</returns>
        public static bool TryGetName<TException>(this TException exception, out string name) where TException : Exception =>

            exception.TryGetValue("Name", out name);

        /// <summary>
        /// Attempts to get and cast a value from the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <typeparam name="T">The exception data value type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="key">The exception data key.</param>
        /// <param name="value">The exception data value.</param>
        /// <returns>The result.</returns>
        public static bool TryGetValue<TException, T>(this TException exception, string key, out T value) where TException : Exception
        {
            try
            {
                value = (T)exception.Data[key]!;
                return !(value == null);
            }
            catch
            {
                value = default!;
                return false;
            }
        }
    }
}
