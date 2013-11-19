// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterConfigStartupTask.cs" company="">
//   
// </copyright>
// <summary>
//   The filter config startup task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI.Installers
{
    using System.Web.Mvc;

    using Bootstrap.Extensions.StartupTasks;

    /// <summary>
    ///     The filter config startup task.
    /// </summary>
    public class FilterConfigStartupTask : IStartupTask
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
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
        }

        #endregion
    }
}