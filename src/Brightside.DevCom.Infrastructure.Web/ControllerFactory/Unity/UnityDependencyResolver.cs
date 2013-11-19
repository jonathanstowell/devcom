// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityDependencyResolver.cs" company="">
//   
// </copyright>
// <summary>
//   The unity dependency resolver.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.ControllerFactory.Unity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The unity dependency resolver.
    /// </summary>
    public class UnityDependencyResolver : IDependencyResolver
    {
        #region Fields

        /// <summary>
        ///     The container.
        /// </summary>
        private readonly IUnityContainer container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public UnityDependencyResolver(IUnityContainer container)
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
        ///     The begin scope.
        /// </summary>
        /// <returns>
        ///     The <see cref="IDependencyScope" />.
        /// </returns>
        public IDependencyScope BeginScope()
        {
            return this;
        }

        /// <summary>
        ///     The dispose.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// The get service.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetService(Type t)
        {
            return this.container.IsRegistered(t) ? this.container.Resolve(t) : null;
        }

        /// <summary>
        /// The get services.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<object> GetServices(Type t)
        {
            return this.container.ResolveAll(t).ToArray();
        }

        #endregion
    }
}