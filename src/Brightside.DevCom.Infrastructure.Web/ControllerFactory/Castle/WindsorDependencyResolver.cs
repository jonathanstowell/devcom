// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindsorDependencyResolver.cs" company="">
//   
// </copyright>
// <summary>
//   The windsor dependency resolver.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.ControllerFactory.Castle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;

    using global::Castle.Windsor;

    /// <summary>
    ///     The windsor dependency resolver.
    /// </summary>
    public class WindsorDependencyResolver : IDependencyResolver
    {
        #region Fields

        /// <summary>
        ///     The container.
        /// </summary>
        private readonly IWindsorContainer container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public WindsorDependencyResolver(IWindsorContainer container)
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
            return new WindsorDependencyScope(this, this.container.Release);
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
            return this.container.Kernel.HasComponent(t) ? this.container.Resolve(t) : null;
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
            return this.container.ResolveAll(t).Cast<object>().ToArray();
        }

        #endregion
    }
}