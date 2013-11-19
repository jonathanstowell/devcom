// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProvidePostValidators.cs" company="">
//   
// </copyright>
// <summary>
//   The provide post validators.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Posts.WebUI.Validations.Impl
{
    using System;

    using Brightside.DevCom.Entities.Posts;
    using Brightside.DevCom.Posts.WebUI.Validations.Validators;

    using FluentValidation;

    /// <summary>
    ///     The provide post validators.
    /// </summary>
    public class ProvidePostValidators : IProvidePostValidators
    {
        #region Fields

        /// <summary>
        ///     The create post validator accessor.
        /// </summary>
        private readonly Func<CreatePostValidator> createPostValidatorAccessor;

        /// <summary>
        ///     The update post validator accessor.
        /// </summary>
        private readonly Func<UpdatePostValidator> updatePostValidatorAccessor;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProvidePostValidators"/> class.
        /// </summary>
        /// <param name="createPostValidatorAccessor">
        /// The create post validator accessor.
        /// </param>
        /// <param name="updatePostValidatorAccessor">
        /// The update post validator accessor.
        /// </param>
        public ProvidePostValidators(
            Func<CreatePostValidator> createPostValidatorAccessor, Func<UpdatePostValidator> updatePostValidatorAccessor)
        {
            if (createPostValidatorAccessor == null)
            {
                throw new ArgumentNullException("createPostValidatorAccessor");
            }

            if (updatePostValidatorAccessor == null)
            {
                throw new ArgumentNullException("updatePostValidatorAccessor");
            }

            this.createPostValidatorAccessor = createPostValidatorAccessor;
            this.updatePostValidatorAccessor = updatePostValidatorAccessor;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the create.
        /// </summary>
        public IValidator<Post> Create
        {
            get
            {
                return this.createPostValidatorAccessor();
            }
        }

        /// <summary>
        ///     Gets the update.
        /// </summary>
        public IValidator<Post> Update
        {
            get
            {
                return this.updatePostValidatorAccessor();
            }
        }

        #endregion
    }
}