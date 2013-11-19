// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindsorControllerFactoryStartupTask.cs" company="">
//   
// </copyright>
// <summary>
//   The windsor controller factory startup task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI.Installers
{
    using System;
    using System.Web.Mvc;

    using Bootstrap;
    using Bootstrap.Extensions.StartupTasks;

    using Brightside.DevCom.Infrastructure.Web.ControllerFactory.Castle;

    using Castle.Windsor;

    /// <summary>
    ///     The windsor controller factory startup task.
    /// </summary>
    public class WindsorControllerFactoryStartupTask : IStartupTask
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
            var controllerFactory = new WindsorControllerFactory((IWindsorContainer)Bootstrapper.Container);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        #endregion
    }
}