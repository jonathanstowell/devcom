// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoFactoryResolver.cs" company="">
//   
// </copyright>
// <summary>
//   The auto factory resolver.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Castle
{
    using System;
    using System.Reflection;

    using global::Castle.Core;

    using global::Castle.MicroKernel;

    using global::Castle.MicroKernel.Context;

    // http://mikehadlow.blogspot.com/2010/01/10-advanced-windsor-tricks-1a-delegate.html
    /// <summary>
    ///     The auto factory resolver.
    /// </summary>
    public class AutoFactoryResolver : ISubDependencyResolver
    {
        #region Fields

        /// <summary>
        ///     The kernel.
        /// </summary>
        private readonly IKernel kernel;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFactoryResolver"/> class.
        /// </summary>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        public AutoFactoryResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The can resolve.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="contextHandlerResolver">
        /// The context handler resolver.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CanResolve(
            CreationContext context, 
            ISubDependencyResolver contextHandlerResolver, 
            ComponentModel model, 
            DependencyModel dependency)
        {
            return dependency.TargetType.IsGenericType
                   && (dependency.TargetType.GetGenericTypeDefinition() == typeof(Func<>))
                   && this.kernel.HasComponent(dependency.TargetType.GetGenericArguments()[0]);
        }

        /// <summary>
        ///     The get resolve delegate.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="Func" />.
        /// </returns>
        public Func<T> GetResolveDelegate<T>()
        {
            return () => this.kernel.Resolve<T>();
        }

        /// <summary>
        /// The resolve.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="contextHandlerResolver">
        /// The context handler resolver.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object Resolve(
            CreationContext context, 
            ISubDependencyResolver contextHandlerResolver, 
            ComponentModel model, 
            DependencyModel dependency)
        {
            MethodInfo getResolveDelegateGeneric = this.GetType().GetMethod("GetResolveDelegate");
            MethodInfo getResolveDelegateMethod =
                getResolveDelegateGeneric.MakeGenericMethod(dependency.TargetType.GetGenericArguments()[0]);
            return getResolveDelegateMethod.Invoke(this, null);
        }

        #endregion
    }
}