// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationResultExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The validation result extenstions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.Extensions
{
    using Brightside.DevCom.Infrastructure.Web.Extensions.Entities;

    using FluentValidation.Results;

    /// <summary>
    ///     The validation result extenstions.
    /// </summary>
    public static class ValidationResultExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The to dto.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResultDTO"/>.
        /// </returns>
        public static ValidationResultDTO ToDTO(this ValidationResult result)
        {
            var ret = new ValidationResultDTO();

            foreach (ValidationFailure error in result.Errors)
            {
                ret.Errors.Add(
                    new ValidationErrorDTO
                        {
                            PropertyName = error.PropertyName, 
                            AttemptedValue =
                                error.AttemptedValue == null ? null : error.AttemptedValue.ToString(), 
                            ErrorMessage = error.ErrorMessage
                        });
            }

            return ret;
        }

        #endregion
    }
}