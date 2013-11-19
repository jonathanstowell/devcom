// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindsorSetupStartupTask.cs" company="">
//   
// </copyright>
// <summary>
//   The windsor setup startup task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI.Installers
{
    using System;

    using Bootstrap;
    using Bootstrap.Extensions.StartupTasks;

    using Brightside.DevCom.Infrastructure.Castle;

    using Castle.MicroKernel.Resolvers.SpecializedResolvers;
    using Castle.Windsor;

    /// <summary>
    ///     The windsor setup startup task.
    /// </summary>
    public class WindsorSetupStartupTask : IStartupTask
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The reset.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void Reset()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     The run.
        /// </summary>
        public void Run()
        {
            ((IWindsorContainer)Bootstrapper.Container).Kernel.Resolver.AddSubResolver(
                new CollectionResolver(((IWindsorContainer)Bootstrapper.Container).Kernel, true));
            ((IWindsorContainer)Bootstrapper.Container).Kernel.Resolver.AddSubResolver(
                new AutoFactoryResolver(((IWindsorContainer)Bootstrapper.Container).Kernel));
        }

        #endregion
    }
}