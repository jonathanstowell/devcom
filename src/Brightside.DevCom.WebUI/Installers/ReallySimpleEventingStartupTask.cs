// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReallySimpleEventingStartupTask.cs" company="">
//   
// </copyright>
// <summary>
//   The really simple eventing startup task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI.Installers
{
    using System;

    using Bootstrap;
    using Bootstrap.Extensions.StartupTasks;

    using Brightside.DevCom.Infrastructure.Eventing;

    using Castle.Windsor;

    using ReallySimpleEventing;

    /// <summary>
    ///     The really simple eventing startup task.
    /// </summary>
    public class ReallySimpleEventingStartupTask : IStartupTask
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
            ReallySimpleEventing.ActivationStrategy =
                new WindsorActivationStrategy((IWindsorContainer)Bootstrapper.Container);
        }

        #endregion
    }
}