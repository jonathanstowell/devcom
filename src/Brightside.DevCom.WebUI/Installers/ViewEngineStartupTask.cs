// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewEngineStartupTask.cs" company="">
//   
// </copyright>
// <summary>
//   The view engine startup task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.WebUI.Installers
{
    using System;
    using System.Web.Mvc;

    using Bootstrap.Extensions.StartupTasks;

    using Brightside.DevCom.Infrastructure.Web.ViewEngines.Plugin;

    /// <summary>
    /// The view engine startup task.
    /// </summary>
    public class ViewEngineStartupTask : IStartupTask
    {
        #region Public Methods and Operators

        /// <summary>
        /// The reset.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void Reset()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The run.
        /// </summary>
        public void Run()
        {
            var engine = new PluginViewEngine();

            engine.AddViewLocationFormat("~/Plugins/Views/{1}/{0}.cshtml");
            engine.AddViewLocationFormat("~/Plugins/Views/{1}/{0}.vbhtml");

            // Add a shared location too, as the lines above are controller specific
            engine.AddPartialViewLocationFormat("~/Plugins/Views/{1}/{0}.cshtml");
            engine.AddPartialViewLocationFormat("~/Plugins/Views/{1}/{0}.vbhtml");

            engine.AddPartialViewLocationFormat("~/Plugins/Views/Shared/{0}.cshtml");
            engine.AddPartialViewLocationFormat("~/Plugins/Views/Shared/{0}.vbhtml");

            ViewEngines.Engines.Add(engine);
        }

        #endregion
    }
}