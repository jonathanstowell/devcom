// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegisterWebApiRoutes.cs" company="">
//   
// </copyright>
// <summary>
//   The RegisterWebApiRoutes interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.Plugin.Routing
{
    using System.Web.Http;

    /// <summary>
    ///     The RegisterWebApiRoutes interface.
    /// </summary>
    public interface IRegisterWebApiRoutes
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        void Register(HttpRouteCollection routes);

        #endregion
    }
}