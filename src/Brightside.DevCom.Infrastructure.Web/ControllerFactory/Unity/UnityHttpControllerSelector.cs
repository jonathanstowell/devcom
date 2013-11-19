// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityHttpControllerSelector.cs" company="">
//   
// </copyright>
// <summary>
//   The unity http controller selector.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.ControllerFactory.Unity
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The unity http controller selector.
    /// </summary>
    public class UnityHttpControllerSelector : DefaultHttpControllerSelector
    {
        #region Fields

        /// <summary>
        ///     The configuration.
        /// </summary>
        private readonly HttpConfiguration configuration;

        /// <summary>
        ///     The container.
        /// </summary>
        private readonly IUnityContainer container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityHttpControllerSelector"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public UnityHttpControllerSelector(HttpConfiguration configuration, IUnityContainer container)
            : base(configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.configuration = configuration;
            this.container = container;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The select controller.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="HttpControllerDescriptor"/>.
        /// </returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            string controllerName = this.GetControllerName(request);
            Type controllerType = this.GetControllerType(controllerName);

            if (controllerType == null)
            {
                return base.SelectController(request);
            }

            return new HttpControllerDescriptor(this.configuration, controllerName, controllerType);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get controller type.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        private Type GetControllerType(string name)
        {
            IHttpController controller = null;

            try
            {
                controller = this.container.Resolve<IHttpController>("api." + name + "Controller");
            }
            catch (Exception)
            {
            }

            if (controller == null)
            {
                try
                {
                    controller =
                        this.container.Resolve<IHttpController>(
                            "api." + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name) + "Controller");
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return controller.GetType();
        }

        #endregion
    }
}