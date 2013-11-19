// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindsorControllerFactory.cs" company="">
//   
// </copyright>
// <summary>
//   The windsor controller factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.ControllerFactory.Castle
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    using global::Castle.Windsor;

    /// <summary>
    ///     The windsor controller factory.
    /// </summary>
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        #region Fields

        /// <summary>
        ///     The container.
        /// </summary>
        private readonly IWindsorContainer container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorControllerFactory"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public WindsorControllerFactory(IWindsorContainer container)
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
                return this.container.Kernel.Resolve<IController>(controllerName + "Controller") as Controller;
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
            this.container.Kernel.ReleaseComponent(controller);
        }

        #endregion
    }
}