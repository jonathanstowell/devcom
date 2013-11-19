// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreatePostValidator.cs" company="">
//   
// </copyright>
// <summary>
//   The create post validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Example.WebUI.Validations.Validators
{
    using Brightside.DevCom.Entities.Example;

    using FluentValidation;

    /// <summary>
    ///     The create post validator.
    /// </summary>
    public class CreatePostValidator : AbstractValidator<Post>
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreatePostValidator" /> class.
        /// </summary>
        public CreatePostValidator()
        {
            this.RuleFor(x => x.Author).NotEmpty();
            this.RuleFor(x => x.Content).NotEmpty();
        }

        #endregion
    }
}