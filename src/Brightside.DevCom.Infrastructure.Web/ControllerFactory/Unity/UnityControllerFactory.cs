// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityControllerFactory.cs" company="">
//   
// </copyright>
// <summary>
//   The unity controller factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.ControllerFactory.Unity
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The unity controller factory.
    /// </summary>
    public class UnityControllerFactory : DefaultControllerFactory
    {
        #region Fields

        /// <summary>
        ///     The container.
        /// </summary>
        private readonly IUnityContainer container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityControllerFactory"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public UnityControllerFactory(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create controller.
        /// </summary>
        /// <param name="requestContext">
        /// The request context.
        /// </param>
        /// <param name="controllerName">
        /// The controller name.
        /// </param>
        /// <returns>
        /// The <see cref="IController"/>.
        /// </returns>
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            try
            {
                return this.container.Resolve<IController>(controllerName + "Controller") as Controller;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// The release controller.
        /// </summary>
        /// <param name="controller">
        /// The controller.
        /// </param>
        public override void ReleaseController(IController controller)
        {
            this.container.Teardown(controller);
        }

        #endregion
    }
}