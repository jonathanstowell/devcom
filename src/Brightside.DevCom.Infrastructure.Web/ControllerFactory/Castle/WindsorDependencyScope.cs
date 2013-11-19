// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindsorDependencyScope.cs" company="">
//   
// </copyright>
// <summary>
//   The windsor dependency scope.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.ControllerFactory.Castle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;

    /// <summary>
    ///     The windsor dependency scope.
    /// </summary>
    public class WindsorDependencyScope : IDependencyScope
    {
        #region Fields

        /// <summary>
        ///     The instances.
        /// </summary>
        private readonly List<object> instances;

        /// <summary>
        ///     The release.
        /// </summary>
        private readonly Action<object> release;

        /// <summary>
        ///     The scope.
        /// </summary>
        private readonly IDependencyScope scope;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorDependencyScope"/> class.
        /// </summary>
        /// <param name="scope">
        /// The scope.
        /// </param>
        /// <param name="release">
        /// The release.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public WindsorDependencyScope(IDependencyScope scope, Action<object> release)
        {
            if (scope == null)
            {
                throw new ArgumentNullException("scope");
            }

            if (release == null)
            {
                throw new ArgumentNullException("release");
            }

            this.scope = scope;
            this.release = release;
            this.instances = new List<object>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The dispose.
        /// </summary>
        public void Dispose()
        {
            foreach (object instance in this.instances)
            {
                this.release(instance);
            }

            this.instances.Clear();
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
            object service = this.scope.GetService(t);
            this.AddToScope(service);
            return service;
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
            IEnumerable<object> services = this.scope.GetServices(t);
            this.AddToScope(services);
            return services;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add to scope.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        private void AddToScope(params object[] services)
        {
            if (services.Any())
            {
                this.instances.AddRange(services);
            }
        }

        #endregion
    }
}