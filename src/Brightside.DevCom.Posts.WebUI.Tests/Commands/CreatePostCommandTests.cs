// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreatePostCommandTests.cs" company="">
//   
// </copyright>
// <summary>
//   The create post command tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Example.WebUI.Tests.Commands
{
    using System.Collections.Generic;

    using Brightside.DevCom.Entities.Posts;
    using Brightside.DevCom.Events.Posts;
    using Brightside.DevCom.Posts.WebUI.Commands;
    using Brightside.DevCom.Posts.WebUI.Validations;
    using Brightside.DevCom.Service;

    using FakeItEasy;

    using FluentValidation.Results;

    using NUnit.Framework;

    using ReallySimpleEventing;

    /// <summary>
    /// The create post command tests.
    /// </summary>
    [TestFixture]
    public class CreatePostCommandTests
    {
        #region Fields

        /// <summary>
        /// The service.
        /// </summary>
        private IPostService service;

        /// <summary>
        /// The stream.
        /// </summary>
        private IEventStream stream;

        /// <summary>
        /// The validators.
        /// </summary>
        private IProvidePostValidators validators;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The invalid post.
        /// </summary>
        [Test]
        public void InvalidPost()
        {
            A.CallTo(() => this.validators.Create.Validate(A<Post>.Ignored))
             .Returns(
                 new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Author", "Mandatory") }));

            var command = new CreatePostCommand(this.stream, this.service, this.validators);

            command.Execute();

            Assert.That(command.IsSuccessful, Is.False);
            Assert.That(command.Results.IsValid, Is.False);
            Assert.That(command.Results.Errors.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.stream = A.Fake<IEventStream>();
            this.service = A.Fake<IPostService>();
            this.validators = A.Fake<IProvidePostValidators>();
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.stream = null;
            this.service = null;
            this.validators = null;
        }

        /// <summary>
        /// The valid post.
        /// </summary>
        [Test]
        public void ValidPost()
        {
            A.CallTo(() => this.validators.Create.Validate(A<Post>.Ignored)).Returns(new ValidationResult());

            A.CallTo(() => this.service.Create(A<Post>.That.Matches(x => x.Author == "Jon" && x.Content == "Test")))
             .Returns(true);

            var command = new CreatePostCommand(this.stream, this.service, this.validators);

            command.Author = "Jon";
            command.Content = "Test";

            command.Execute();

            Assert.That(command.IsSuccessful, Is.True);
            Assert.That(command.Results.IsValid, Is.True);
            Assert.That(command.Results.Errors.Count, Is.EqualTo(0));
            A.CallTo(() => this.service.Create(A<Post>.That.Matches(x => x.Author == "Jon" && x.Content == "Test")))
             .MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => this.stream.Raise(A<PostWasCreated>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        #endregion
    }
}