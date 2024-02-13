using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Datapoint
{
    /// <summary>
    /// Exception extensions.
    /// </summary>
    public static class ExceptionExtensions
    {
        private const string CorrelationId = nameof(CorrelationId);

        private const string ErrorCode = nameof(ErrorCode);

        private const string ErrorMessage = nameof(ErrorMessage);

        private const string InnerErrors = nameof(InnerErrors);

        private const string Id = nameof(Id);

        /// <summary>
        /// Adds the exception correlation identifier to the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <returns>The exception.</returns>
        public static TException WithCorrelationId<TException>(this TException exception, Guid correlationId) where TException : Exception =>

            exception.WithCorrelationId(correlationId.ToString());

        /// <summary>
        /// Adds the exception identifier to the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <returns>The exception.</returns>
        public static TException WithCorrelationId<TException>(this TException exception, string correlationId) where TException : Exception
        {
            exception.Data[CorrelationId] = correlationId;

            return exception;
        }

        /// <summary>
        /// Adds the error code to the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="errorCode">The error code.</param>
        /// <returns>The exception.</returns>
        public static TException WithErrorCode<TException>(this TException exception, string errorCode) where TException : Exception
        {
            exception.Data[ErrorCode] = errorCode;

            return exception;
        }

        /// <summary>
        /// Adds the error message to the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The exception.</returns>
        public static TException WithErrorMessage<TException>(this TException exception, string errorMessage) where TException : Exception
        {
            exception.Data[ErrorMessage] = errorMessage;

            return exception;
        }

        /// <summary>
        /// Adds the exception identifier to the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>The exception.</returns>
        public static TException WithId<TException>(this TException exception, Guid id) where TException : Exception =>

            exception.WithId(id.ToString());

        /// <summary>
        /// Adds the exception identifier to the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>The exception.</returns>
        public static TException WithId<TException>(this TException exception, string id) where TException : Exception
        {
            exception.Data[Id] = id;

            return exception;
        }

        /// <summary>
        /// Adds an inner error to the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="propertyName">The error property name.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The exception.</returns>
        public static TException WithInnerError<TException>(this TException exception, string propertyName, string? errorCode, string errorMessage) where TException : Exception
        {
            if (!exception.Data.Contains(InnerErrors))
                exception.Data[InnerErrors] = new Dictionary<string, List<Error>>();

            var innerErrors = (Dictionary<string, List<Error>>) exception.Data[InnerErrors]!;

            if (!innerErrors.TryGetValue(propertyName, out var propertyInnerErrors))
                innerErrors.Add(propertyName, propertyInnerErrors = new List<Error>(1));

            propertyInnerErrors.Add(
                new Error(
                    errorCode,
                    errorMessage));

            return exception;
        }

        /// <summary>
        /// Gets the exception error code.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="errorCode">The exception error code.</param>
        /// <returns>Success status.</returns>
        public static bool TryGetErrorCode<TException>(this TException exception, out string errorCode) where TException : Exception
        {
            if (exception.Data.Contains(ErrorCode))
            {
                if (exception.Data[ErrorCode] is string && !string.IsNullOrEmpty((string) exception.Data[ErrorCode]!))
                {
                    errorCode = (string) exception.Data[ErrorCode]!;
                    return true;
                }
            }

            errorCode = default!;
            return false;
        }

        /// <summary>
        /// Gets the exception error message.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="errorMessage">The exception error message.</param>
        /// <returns>Success status.</returns>
        public static bool TryGetErrorMessage<TException>(this TException exception, out string errorMessage) where TException : Exception
        {
            if (exception.Data.Contains(ErrorMessage))
            {
                if (exception.Data[ErrorMessage] is string && !string.IsNullOrEmpty((string) exception.Data[ErrorMessage]!))
                {
                    errorMessage = (string) exception.Data[ErrorMessage]!;
                    return true;
                }
            }

            errorMessage = default!;
            return false;
        }

        /// <summary>
        /// Gets the exception identifier.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="correlationId">The exception correlation identifier.</param>
        /// <returns>Success status.</returns>
        public static bool TryGetCorrelationId<TException>(this TException exception, out string correlationId) where TException : Exception
        {
            if (exception.Data.Contains(CorrelationId))
            {
                if (exception.Data[CorrelationId] is string && !string.IsNullOrEmpty((string) exception.Data[CorrelationId]!))
                {
                    correlationId = (string) exception.Data[CorrelationId]!;
                    return true;
                }
            }

            correlationId = default!;
            return false;
        }

        /// <summary>
        /// Gets the exception identifier.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="id">The exception identifier.</param>
        /// <returns>Success status.</returns>
        public static bool TryGetId<TException>(this TException exception, out string id) where TException : Exception
        {
            if (exception.Data.Contains(Id))
            {
                if (exception.Data[Id] is string && !string.IsNullOrEmpty((string) exception.Data[Id]!))
                {
                    id = (string) exception.Data[Id]!;
                    return true;
                }
            }

            id = default!;
            return false;
        }

        /// <summary>
        /// Gets the exception inner errors.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="innerErrors">The exception inner errors.</param>
        /// <returns>Success status.</returns>
        public static bool TryGetInnerErrors<TException>(this TException exception, out IReadOnlyDictionary<string, IReadOnlyCollection<Error>> innerErrors) where TException : Exception
        {
            if (exception.Data.Contains(InnerErrors))
            {
                if (exception.Data[InnerErrors] is Dictionary<string, List<Error>>)
                {
                    innerErrors = ((Dictionary<string, List<Error>>) exception.Data[InnerErrors]!)
                        .ToDictionary(
                            kv => kv.Key,
                            kv => (IReadOnlyCollection<Error>) kv.Value.ToArray());

                    return true;
                }
            }

            innerErrors = default!;
            return false;
        }
    }
}
