// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="">
//   
// </copyright>
// <summary>
//   The mvc application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.SessionState;

    using Bootstrap;
    using Bootstrap.Extensions.Containers;
    using Bootstrap.Extensions.StartupTasks;

    using Brightside.DevCom.Infrastructure.Bootstrapping;
    using Brightside.DevCom.Infrastructure.Plugin;
    using Brightside.DevCom.Infrastructure.Plugin.Impl;
    using Brightside.DevCom.Infrastructure.Web.Helpers;
    using Brightside.DevCom.WebUI.Installers;

    using Castle.Windsor;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    ///     The mvc application.
    /// </summary>
    public class MvcApplication : HttpApplication, IRequiresSessionState
    {
        #region Static Fields

        /// <summary>
        ///     The container.
        /// </summary>
        public static IWindsorContainer Container;

        /// <summary>
        ///     The plugin assembly provider.
        /// </summary>
        public static IPluginAssemblyProvider PluginAssemblyProvider = new PluginAssemblyProvider(
            "Plugins", "Brightside.*.dll");

        #endregion

        #region Properties

        /// <summary>
        /// Gets the request context.
        /// </summary>
        protected IProvideRequestContext RequestContext
        {
            get
            {
                return Container.Resolve<IProvideRequestContext>();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The application_ start.
        /// </summary>
        protected void Application_Start()
        {
            Bootstrapper.With.PluggableWindsor(new RegistrationHelper(PluginAssemblyProvider))
                        .And.StartupTasks()
                        .UsingThisExecutionOrder(
                            s =>
                            s.First<WebUIStartupTask>()
                             .Then<SignalRStartupTask>()
                             .Then<RegisterRoutesStartupTask>()
                             .Then()
                             .TheRest())
                        .Start();

            Container = (IWindsorContainer)Bootstrapper.Container;

            AreaRegistration.RegisterAllAreas();
        }

        #endregion
    }
}