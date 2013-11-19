// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreatePostValidatorTests.cs" company="">
//   
// </copyright>
// <summary>
//   The create post validator tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Example.WebUI.Tests.Validators
{
    using System.Linq;

    using Brightside.DevCom.Entities.Posts;
    using Brightside.DevCom.Posts.WebUI.Validations.Validators;

    using FluentValidation.Results;

    using NUnit.Framework;

    /// <summary>
    /// The create post validator tests.
    /// </summary>
    [TestFixture]
    public class CreatePostValidatorTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The empty author invalid.
        /// </summary>
        [Test]
        public void EmptyAuthorInvalid()
        {
            var validator = new CreatePostValidator();

            ValidationResult result = validator.Validate(new Post { Author = string.Empty, Content = "Test" });

            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsValid, Is.False);
            Assert.That(
                result.Errors.Count(x => x.PropertyName == "Author" && x.ErrorMessage.Contains("empty")), Is.EqualTo(1));
        }

        /// <summary>
        /// The empty content invalid.
        /// </summary>
        [Test]
        public void EmptyContentInvalid()
        {
            var validator = new CreatePostValidator();

            ValidationResult result = validator.Validate(new Post { Author = "Test", Content = string.Empty });

            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsValid, Is.False);
            Assert.That(
                result.Errors.Count(x => x.PropertyName == "Content" && x.ErrorMessage.Contains("empty")), Is.EqualTo(1));
        }

        /// <summary>
        /// The null author invalid.
        /// </summary>
        [Test]
        public void NullAuthorInvalid()
        {
            var validator = new CreatePostValidator();

            ValidationResult result = validator.Validate(new Post { Author = null, Content = "Test" });

            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsValid, Is.False);
            Assert.That(
                result.Errors.Count(x => x.PropertyName == "Author" && x.ErrorMessage.Contains("empty")), Is.EqualTo(1));
        }

        /// <summary>
        /// The null content invalid.
        /// </summary>
        [Test]
        public void NullContentInvalid()
        {
            var validator = new CreatePostValidator();

            ValidationResult result = validator.Validate(new Post { Author = "Test", Content = null });

            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsValid, Is.False);
            Assert.That(
                result.Errors.Count(x => x.PropertyName == "Content" && x.ErrorMessage.Contains("empty")), Is.EqualTo(1));
        }

        /// <summary>
        /// The valid.
        /// </summary>
        [Test]
        public void Valid()
        {
            var validator = new CreatePostValidator();

            ValidationResult result = validator.Validate(new Post { Author = "Test", Content = "Test" });

            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors.Count, Is.EqualTo(0));
        }

        #endregion
    }
}