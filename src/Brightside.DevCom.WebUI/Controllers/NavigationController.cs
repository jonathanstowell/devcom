// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationController.cs" company="">
//   
// </copyright>
// <summary>
//   The post controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI.Controllers
{
    using System.Web.Mvc;

    using Brightside.DevCom.Infrastructure.Web.Plugin.Navigation;
    using Brightside.DevCom.WebUI.Models;

    /// <summary>
    ///     The post controller.
    /// </summary>
    public class NavigationController : Controller
    {
        #region Fields

        /// <summary>
        /// The navigation.
        /// </summary>
        private readonly IProvideNavigationTree navigation;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationController"/> class.
        /// </summary>
        /// <param name="navigation">
        /// The navigation.
        /// </param>
        public NavigationController(IProvideNavigationTree navigation)
        {
            this.navigation = navigation;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The index.
        /// </summary>
        /// <returns>
        ///     The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            return this.PartialView(new NavigationViewModel { Navigation = this.navigation.GetNavigation() });
        }

        #endregion
    }
}