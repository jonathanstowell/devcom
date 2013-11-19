// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdatePostValidatorTests.cs" company="">
//   
// </copyright>
// <summary>
//   The update post validator tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Example.WebUI.Tests.Validators
{
    using System.Linq;

    using Brightside.DevCom.Entities.Example;
    using Brightside.DevCom.Example.WebUI.Validations.Validators;

    using FluentValidation.Results;

    using NUnit.Framework;

    /// <summary>
    /// The update post validator tests.
    /// </summary>
    [TestFixture]
    public class UpdatePostValidatorTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The empty author invalid.
        /// </summary>
        [Test]
        public void EmptyAuthorInvalid()
        {
            var validator = new UpdatePostValidator();

            ValidationResult result = validator.Validate(new Post { Id = 1, Author = string.Empty, Content = "Test" });

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
            var validator = new UpdatePostValidator();

            ValidationResult result = validator.Validate(new Post { Id = 1, Author = "Test", Content = string.Empty });

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
            var validator = new UpdatePostValidator();

            ValidationResult result = validator.Validate(new Post { Id = 1, Author = null, Content = "Test" });

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
            var validator = new UpdatePostValidator();

            ValidationResult result = validator.Validate(new Post { Id = 1, Author = "Test", Content = null });

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
            var validator = new UpdatePostValidator();

            ValidationResult result = validator.Validate(new Post { Id = 1, Author = "Test", Content = "Test" });

            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Errors.Count, Is.EqualTo(0));
        }

        #endregion
    }
}