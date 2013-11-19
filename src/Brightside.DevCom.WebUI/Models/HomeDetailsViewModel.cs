// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeDetailsViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The home details view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI.Models
{
    using System;

    /// <summary>
    ///     The home details view model.
    /// </summary>
    public class HomeDetailsViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the installation type.
        /// </summary>
        public Type InstallationType { get; set; }

        /// <summary>
        ///     Gets or sets the session test.
        /// </summary>
        public string SessionTest { get; set; }

        #endregion
    }
}