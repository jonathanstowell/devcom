// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProvidePostValidators.cs" company="">
//   
// </copyright>
// <summary>
//   The ProvidePostValidators interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Example.WebUI.Validations
{
    using Brightside.DevCom.Entities.Example;

    using FluentValidation;

    /// <summary>
    ///     The ProvidePostValidators interface.
    /// </summary>
    public interface IProvidePostValidators
    {
        #region Public Properties

        /// <summary>
        ///     Gets the create.
        /// </summary>
        IValidator<Post> Create { get; }

        /// <summary>
        ///     Gets the update.
        /// </summary>
        IValidator<Post> Update { get; }

        #endregion
    }
}