// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostControllerTests.cs" company="">
//   
// </copyright>
// <summary>
//   The post controller tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Example.WebUI.Tests.Controllers
{
    using System.Web.Mvc;

    using Brightside.DevCom.Posts.WebUI.Controllers;

    using NUnit.Framework;

    /// <summary>
    /// The post controller tests.
    /// </summary>
    [TestFixture]
    public class PostControllerTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The index returns view.
        /// </summary>
        [Test]
        public void IndexReturnsView()
        {
            var controller = new PostController();

            var result = controller.Index() as ViewResult;

            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        #endregion
    }
}