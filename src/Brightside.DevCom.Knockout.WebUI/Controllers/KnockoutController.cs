// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnockoutController.cs" company="">
//   
// </copyright>
// <summary>
//   The post controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Knockout.WebUI.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    ///     The post controller.
    /// </summary>
    public class KnockoutController : Controller
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

        public ActionResult Computed()
        {
            return this.View("Computed");
        }

        #endregion
    }
}