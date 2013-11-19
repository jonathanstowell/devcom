// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppDomainAssemblyResolverStartupTask.cs" company="">
//   
// </copyright>
// <summary>
//   The app domain assembly resolver startup task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI.Installers
{
    using System;

    using Bootstrap.Extensions.StartupTasks;

    /// <summary>
    ///     The app domain assembly resolver startup task.
    /// </summary>
    public class AppDomainAssemblyResolverStartupTask : IStartupTask
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
            AppDomain.CurrentDomain.AssemblyResolve +=
                (a, args) => MvcApplication.PluginAssemblyProvider.GetAssembly(args.Name);
        }

        #endregion
    }
}