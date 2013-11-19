// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreatePostCommand.cs" company="">
//   
// </copyright>
// <summary>
//   The create post command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Posts.WebUI.Commands
{
    using Brightside.DevCom.Entities.Posts;
    using Brightside.DevCom.Events.Posts;
    using Brightside.DevCom.Infrastructure.Eventing;
    using Brightside.DevCom.Infrastructure.Web.Conventions;
    using Brightside.DevCom.Posts.WebUI.Validations;
    using Brightside.DevCom.Service;

    using FluentValidation.Results;

    using ReallySimpleEventing;

    /// <summary>
    ///     The create post command.
    /// </summary>
    public class CreatePostCommand : ICommand
    {
        #region Fields

        /// <summary>
        ///     The service.
        /// </summary>
        private readonly IPostService service;

        /// <summary>
        ///     The stream.
        /// </summary>
        private readonly IEventStream stream;

        /// <summary>
        ///     The validators.
        /// </summary>
        private readonly IProvidePostValidators validators;

        /// <summary>
        ///     The executed.
        /// </summary>
        private bool executed;

        /// <summary>
        ///     The successful.
        /// </summary>
        private bool successful;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePostCommand"/> class.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <param name="service">
        /// The service.
        /// </param>
        /// <param name="validators">
        /// The validators.
        /// </param>
        public CreatePostCommand(IEventStream stream, IPostService service, IProvidePostValidators validators)
        {
            this.stream = stream;
            this.service = service;
            this.validators = validators;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     Gets or sets the content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     Gets a value indicating whether has executed.
        /// </summary>
        public bool HasExecuted
        {
            get
            {
                return this.executed;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether is successful.
        /// </summary>
        public bool IsSuccessful
        {
            get
            {
                return this.successful;
            }
        }

        /// <summary>
        ///     Gets the post.
        /// </summary>
        public Post Post { get; private set; }

        /// <summary>
        ///     Gets or sets the results.
        /// </summary>
        public ValidationResult Results { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The execute.
        /// </summary>
        public void Execute()
        {
            this.executed = true;

            this.Validate();

            if (this.Results.IsValid)
            {
                this.successful = this.service.Create(this.Post);

                if (this.successful)
                {
                    this.stream.Raise<PostWasCreated>(x => { x.Post = this.Post; });
                }
            }
        }

        /// <summary>
        ///     The validate.
        /// </summary>
        public void Validate()
        {
            this.Prime();
            this.Results = this.validators.Create.Validate(this.Post);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The prime.
        /// </summary>
        private void Prime()
        {
            this.Post = new Post { Author = this.Author, Content = this.Content };
        }

        #endregion
    }
}