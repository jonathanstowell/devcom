// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindsorHttpControllerActivator.cs" company="">
//   
// </copyright>
// <summary>
//   The windsor http controller activator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.ControllerFactory.Castle
{
    using System;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;

    using global::Castle.Windsor;

    /// <summary>
    ///     The windsor http controller activator.
    /// </summary>
    public class WindsorHttpControllerActivator : IHttpControllerActivator
    {
        #region Fields

        /// <summary>
        ///     The container.
        /// </summary>
        private readonly IWindsorContainer container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorHttpControllerActivator"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public WindsorHttpControllerActivator(IWindsorContainer container)
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

            request.RegisterForDispose(new Release(() => this.container.Release(controller)));

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