// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleStartupTask.cs" company="">
//   
// </copyright>
// <summary>
//   The bundle startup task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI.Installers
{
    using System.Web.Optimization;

    using Bootstrap.Extensions.StartupTasks;

    /// <summary>
    ///     The bundle startup task.
    /// </summary>
    public class BundleStartupTask : IStartupTask
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The reset.
        /// </summary>
        public void Reset()
        {
        }

        /// <summary>
        ///     The run.
        /// </summary>
        public void Run()
        {
            BundleCollection bundles = BundleTable.Bundles;

            bundles.Add(new ScriptBundle("~/js/jquery").Include("~/js/libs/jquery-{version}.js"));

            bundles.Add(
                new ScriptBundle("~/js/all").Include(
                    "~/js/libs/jquery.signalR-{version}.js", 
                    "~/js/libs/bootstrap.js", 
                    "~/js/libs/knockout-2.2.0.js", 
                    "~/js/libs/knockout.mapping-2.2.0.js", 
                    "~/js/libs/knockout.validation.js", 
                    "~/js/mylibs/brightside.knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/js/signalr").Include("~/js/libs/jquery.signalR-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/js/modernizr").Include("~/js/libs/modernizr-*"));

            Bundle bootstrapBundle = new Bundle("~/css/bootstrap").Include(
                "~/css/libs/bootstrap/bootstrap.flatly.css", "~/css/libs/bootstrap/bootstrap-responsive.css");

            bundles.Add(bootstrapBundle);

            bundles.Add(new StyleBundle("~/css/fontawesome").Include("~/css/libs/fonts/font-awesome.css"));

            bundles.Add(new StyleBundle("~/css/styles").Include("~/css/mylibs/styles.css"));
        }

        #endregion
    }
}