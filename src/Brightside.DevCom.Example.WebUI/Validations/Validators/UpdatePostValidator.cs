// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdatePostValidator.cs" company="">
//   
// </copyright>
// <summary>
//   The update post validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Example.WebUI.Validations.Validators
{
    using Brightside.DevCom.Entities.Example;

    using FluentValidation;

    /// <summary>
    ///     The update post validator.
    /// </summary>
    public class UpdatePostValidator : AbstractValidator<Post>
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UpdatePostValidator" /> class.
        /// </summary>
        public UpdatePostValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty();
            this.RuleFor(x => x.Author).NotEmpty();
            this.RuleFor(x => x.Content).NotEmpty();
        }

        #endregion
    }
}