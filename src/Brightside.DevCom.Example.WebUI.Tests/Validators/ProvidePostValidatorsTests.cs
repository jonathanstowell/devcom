// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProvidePostValidatorsTests.cs" company="">
//   
// </copyright>
// <summary>
//   The provide post validators tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Example.WebUI.Tests.Validators
{
    using System;

    using Brightside.DevCom.Entities.Example;
    using Brightside.DevCom.Example.WebUI.Validations.Impl;
    using Brightside.DevCom.Example.WebUI.Validations.Validators;

    using FakeItEasy;

    using FluentValidation;

    using NUnit.Framework;

    /// <summary>
    /// The provide post validators tests.
    /// </summary>
    [TestFixture]
    public class ProvidePostValidatorsTests
    {
        #region Fields

        /// <summary>
        /// The create validator.
        /// </summary>
        private CreatePostValidator createValidator;

        /// <summary>
        /// The update validator.
        /// </summary>
        private UpdatePostValidator updateValidator;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create returns validator.
        /// </summary>
        [Test]
        public void CreateReturnsValidator()
        {
            var validators = new ProvidePostValidators(() => this.createValidator, () => this.updateValidator);

            IValidator<Post> result = validators.Create;

            Assert.That(result, Is.Not.Null);
        }

        /// <summary>
        /// The null create throws exception.
        /// </summary>
        [Test]
        public void NullCreateThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProvidePostValidators(null, () => this.updateValidator));
        }

        /// <summary>
        /// The null update throws exception.
        /// </summary>
        [Test]
        public void NullUpdateThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProvidePostValidators(() => this.createValidator, null));
        }

        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.createValidator = A.Fake<CreatePostValidator>();
            this.updateValidator = A.Fake<UpdatePostValidator>();
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.createValidator = null;
            this.updateValidator = null;
        }

        /// <summary>
        /// The update returns validator.
        /// </summary>
        [Test]
        public void UpdateReturnsValidator()
        {
            var validators = new ProvidePostValidators(() => this.createValidator, () => this.updateValidator);

            IValidator<Post> result = validators.Update;

            Assert.That(result, Is.Not.Null);
        }

        #endregion
    }
}