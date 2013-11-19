// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdatePostCommandTests.cs" company="">
//   
// </copyright>
// <summary>
//   The update post command tests.
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
    /// The update post command tests.
    /// </summary>
    [TestFixture]
    public class UpdatePostCommandTests
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
            A.CallTo(() => this.service.GetById(1)).Returns(new Post { Id = 1, Author = "Jon", Content = "Test" });
            A.CallTo(
                () =>
                this.validators.Update.Validate(
                    A<Post>.That.Matches(x => x.Id == 1 && x.Author == string.Empty && x.Content == "Welcome")))
             .Returns(
                 new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Author", "Mandatory") }));

            var command = new UpdatePostCommand(this.stream, this.service, this.validators);

            command.Id = 1;
            command.Author = string.Empty;
            command.Content = "Welcome";

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
            A.CallTo(() => this.service.GetById(1)).Returns(new Post { Id = 1, Author = "Jon", Content = "Test" });
            A.CallTo(() => this.validators.Update.Validate(A<Post>.Ignored)).Returns(new ValidationResult());

            A.CallTo(
                () => this.service.Update(A<Post>.That.Matches(x => x.Author == "David" && x.Content == "Welcome")))
             .Returns(true);

            var command = new UpdatePostCommand(this.stream, this.service, this.validators);

            command.Id = 1;
            command.Author = "David";
            command.Content = "Welcome";

            command.Execute();

            Assert.That(command.IsSuccessful, Is.True);
            Assert.That(command.Results.IsValid, Is.True);
            Assert.That(command.Results.Errors.Count, Is.EqualTo(0));
            A.CallTo(
                () => this.service.Update(A<Post>.That.Matches(x => x.Author == "David" && x.Content == "Welcome")))
             .MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => this.stream.Raise(A<PostWasUpdated>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        #endregion
    }
}