// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginViewEngine.cs" company="">
//   
// </copyright>
// <summary>
//   The plugin view engine.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.ViewEngines.Plugin
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    ///     The plugin view engine.
    /// </summary>
    public class PluginViewEngine : RazorViewEngine
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add partial view location format.
        /// </summary>
        /// <param name="paths">
        /// The paths.
        /// </param>
        public void AddPartialViewLocationFormat(string paths)
        {
            var existingPaths = new List<string>(this.PartialViewLocationFormats) { paths };
            this.PartialViewLocationFormats = existingPaths.ToArray();
        }

        /// <summary>
        /// The add view location format.
        /// </summary>
        /// <param name="paths">
        /// The paths.
        /// </param>
        public void AddViewLocationFormat(string paths)
        {
            var existingPaths = new List<string>(this.ViewLocationFormats) { paths };
            this.ViewLocationFormats = existingPaths.ToArray();
        }

        #endregion
    }
}