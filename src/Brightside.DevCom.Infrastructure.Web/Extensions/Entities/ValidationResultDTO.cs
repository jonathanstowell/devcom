// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationResultDTO.cs" company="">
//   
// </copyright>
// <summary>
//   The validation result dto.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.Extensions.Entities
{
    using System.Collections.Generic;

    /// <summary>
    ///     The validation result dto.
    /// </summary>
    public class ValidationResultDTO
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidationResultDTO" /> class.
        /// </summary>
        public ValidationResultDTO()
        {
            this.Errors = new List<ValidationErrorDTO>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the errors.
        /// </summary>
        public IList<ValidationErrorDTO> Errors { get; set; }

        /// <summary>
        ///     Gets a value indicating whether is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.Errors.Count == 0;
            }
        }

        #endregion
    }

    /// <summary>
    ///     The validation error dto.
    /// </summary>
    public class ValidationErrorDTO
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the attempted value.
        /// </summary>
        public string AttemptedValue { get; set; }

        /// <summary>
        ///     Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     Gets or sets the property name.
        /// </summary>
        public string PropertyName { get; set; }

        #endregion
    }
}