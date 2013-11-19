// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindsorWebApiStartupTask.cs" company="">
//   
// </copyright>
// <summary>
//   The windsor web api startup task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI.Installers
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;

    using Bootstrap;
    using Bootstrap.Extensions.StartupTasks;

    using Brightside.DevCom.Infrastructure.Web.ControllerFactory.Castle;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    /// <summary>
    ///     The windsor web api startup task.
    /// </summary>
    public class WindsorWebApiStartupTask : IStartupTask
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
            ((IWindsorContainer)Bootstrapper.Container).Register(
                Component.For<IHttpControllerSelector>()
                         .Instance(
                             new WindsorHttpControllerSelector(
                                 GlobalConfiguration.Configuration, (IWindsorContainer)Bootstrapper.Container)));
            GlobalConfiguration.Configuration.DependencyResolver =
                new WindsorDependencyResolver((IWindsorContainer)Bootstrapper.Container);
            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator), 
                new WindsorHttpControllerActivator((IWindsorContainer)Bootstrapper.Container));
        }

        #endregion
    }
}