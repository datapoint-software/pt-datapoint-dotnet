using System;
using System.ComponentModel.DataAnnotations;

namespace Datapoint.FluentValidation.Helpers
{
    /// <summary>
    /// A helper for <see cref="Exception" />.
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Adds a validation result to the exception data.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="validationResult">The validation result.</param>
        /// <returns>The exception.</returns>
        public static TException AddValidationResult<TException>(this TException exception, ValidationResult validationResult) where TException : Exception
        {
            exception.Data.Add(typeof(ValidationResult).Name, validationResult);
            return exception;
        }
    }
}
