// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostController.cs" company="">
//   
// </copyright>
// <summary>
//   The post controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Example.WebUI.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    ///     The post controller.
    /// </summary>
    public class PostController : Controller
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The index.
        /// </summary>
        /// <returns>
        ///     The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            return this.View("Index");
        }

        #endregion
    }
}