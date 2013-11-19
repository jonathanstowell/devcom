// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegisterRoutes.cs" company="">
//   
// </copyright>
// <summary>
//   The RegisterRoutes interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.Plugin.Routing
{
    using System.Web.Routing;

    /// <summary>
    ///     The RegisterRoutes interface.
    /// </summary>
    public interface IRegisterRoutes
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        void Register(RouteCollection routes);

        #endregion
    }
}