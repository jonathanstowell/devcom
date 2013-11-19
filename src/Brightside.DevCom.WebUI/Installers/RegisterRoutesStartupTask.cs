// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterRoutesStartupTask.cs" company="">
//   
// </copyright>
// <summary>
//   The register routes startup task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI.Installers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Bootstrap;
    using Bootstrap.Extensions.StartupTasks;

    using Brightside.DevCom.Infrastructure.Web.Plugin.Routing;

    /// <summary>
    ///     The register routes startup task.
    /// </summary>
    public class RegisterRoutesStartupTask : IStartupTask
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
            foreach (IRegisterRoutes installer in Bootstrapper.ContainerExtension.ResolveAll<IRegisterRoutes>().ToList()
                )
            {
                installer.Register(RouteTable.Routes);
            }

            foreach (IRegisterWebApiRoutes installer in
                Bootstrapper.ContainerExtension.ResolveAll<IRegisterWebApiRoutes>().ToList())
            {
                installer.Register(GlobalConfiguration.Configuration.Routes);
            }

            this.RegisterDefaults();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The register api defaults.
        /// </summary>
        private void RegisterApiDefaults()
        {
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi", 
                routeTemplate: "api/{controller}/{id}", 
                defaults: new { id = RouteParameter.Optional });
        }

        /// <summary>
        ///     The register defaults.
        /// </summary>
        private void RegisterDefaults()
        {
            this.RegisterApiDefaults();
            this.RegisterMvcDefaults();
        }

        /// <summary>
        ///     The register mvc defaults.
        /// </summary>
        private void RegisterMvcDefaults()
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            RouteTable.Routes.MapRoute(
                name: "Default", 
                url: "{controller}/{action}/{id}", 
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            RouteTable.Routes.MapRoute(
                name: "ControllerLessToHome", 
                url: "{action}/{id}", 
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        #endregion
    }
}