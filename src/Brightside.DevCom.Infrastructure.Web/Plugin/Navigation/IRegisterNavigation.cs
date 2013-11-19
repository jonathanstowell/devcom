// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegisterNavigation.cs" company="">
//   
// </copyright>
// <summary>
//   The RegisterNavigation interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.Plugin.Navigation
{
    using Brightside.DevCom.Infrastructure.Web.Plugin.Navigation.Entities;

    /// <summary>
    ///     The RegisterNavigation interface.
    /// </summary>
    public interface IRegisterNavigation
    {
        #region Public Properties

        /// <summary>
        ///     Gets the path.
        /// </summary>
        NavigationNode Path { get; }

        /// <summary>
        ///     Gets the roles.
        /// </summary>
        string[] Permissions { get; }

        /// <summary>
        ///     Gets a value indicating whether require all roles.
        /// </summary>
        bool RequireAllPermissions { get; }

        #endregion
    }
}