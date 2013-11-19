// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityHttpControllerActivator.cs" company="">
//   
// </copyright>
// <summary>
//   The unity http controller activator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.ControllerFactory.Unity
{
    using System;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The unity http controller activator.
    /// </summary>
    public class UnityHttpControllerActivator : IHttpControllerActivator
    {
        #region Fields

        /// <summary>
        ///     The container.
        /// </summary>
        private readonly IUnityContainer container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityHttpControllerActivator"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public UnityHttpControllerActivator(IUnityContainer container)
        {
            this.container = container;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="controllerDescriptor">
        /// The controller descriptor.
        /// </param>
        /// <param name="controllerType">
        /// The controller type.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpController"/>.
        /// </returns>
        public IHttpController Create(
            HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            IHttpController controller;

            try
            {
                controller = this.container.Resolve<IHttpController>("api." + controllerType.Name);
            }
            catch (Exception)
            {
                controller = null;
            }

            request.RegisterForDispose(new Release(() => this.container.Teardown(controller)));

            return controller;
        }

        #endregion

        /// <summary>
        ///     The release.
        /// </summary>
        private class Release : IDisposable
        {
            #region Fields

            /// <summary>
            ///     The release.
            /// </summary>
            private readonly Action release;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Release"/> class.
            /// </summary>
            /// <param name="release">
            /// The release.
            /// </param>
            public Release(Action release)
            {
                this.release = release;
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            ///     The dispose.
            /// </summary>
            public void Dispose()
            {
                this.release();
            }

            #endregion
        }
    }
}