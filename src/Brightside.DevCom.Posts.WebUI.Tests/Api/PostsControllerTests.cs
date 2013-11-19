// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostsControllerTests.cs" company="">
//   
// </copyright>
// <summary>
//   The posts controller tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Example.WebUI.Tests.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Brightside.DevCom.Entities.Posts;
    using Brightside.DevCom.Posts.WebUI.Api;
    using Brightside.DevCom.Posts.WebUI.Commands;
    using Brightside.DevCom.Service;

    using FakeItEasy;

    using Microsoft.AspNet.SignalR.Infrastructure;

    using NUnit.Framework;

    /// <summary>
    /// The posts controller tests.
    /// </summary>
    [TestFixture]
    public class PostsControllerTests
    {
        #region Fields

        /// <summary>
        /// The connection.
        /// </summary>
        private IConnectionManager connection;

        /// <summary>
        /// The create command.
        /// </summary>
        private CreatePostCommand createCommand;

        /// <summary>
        /// The service.
        /// </summary>
        private IPostService service;

        /// <summary>
        /// The update command.
        /// </summary>
        private UpdatePostCommand updateCommand;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get all posts.
        /// </summary>
        [Test]
        public void GetAllPosts()
        {
            A.CallTo(() => this.service.Get()).Returns(new List<Post> { new Post { Id = 1 } });

            var contoller = new PostsController(
                this.service, () => this.createCommand, () => this.updateCommand, this.connection);

            IList<Post> result = contoller.Get();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo(1));
        }

        /// <summary>
        /// The get existing post.
        /// </summary>
        [Test]
        public void GetExistingPost()
        {
            A.CallTo(() => this.service.GetById(1)).Returns(new Post { Id = 1 });

            var contoller = new PostsController(
                this.service, () => this.createCommand, () => this.updateCommand, this.connection);

            Post result = contoller.Get(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        /// <summary>
        /// The get non existing post.
        /// </summary>
        [Test]
        public void GetNonExistingPost()
        {
            A.CallTo(() => this.service.GetById(1)).Returns(null);

            var contoller = new PostsController(
                this.service, () => this.createCommand, () => this.updateCommand, this.connection);

            Assert.Throws<HttpResponseException>(() => contoller.Get(1));
        }

        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.service = A.Fake<IPostService>();
            this.createCommand = A.Fake<CreatePostCommand>();
            this.updateCommand = A.Fake<UpdatePostCommand>();
            this.connection = A.Fake<IConnectionManager>();
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.service = null;
            this.createCommand = null;
            this.updateCommand = null;
            this.connection = null;
        }

        #endregion
    }
}