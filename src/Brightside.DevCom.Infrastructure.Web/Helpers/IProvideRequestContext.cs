// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProvideRequestContext.cs" company="">
//   
// </copyright>
// <summary>
//   The ProvideRequestContext interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Infrastructure.Web.Helpers
{
    /// <summary>
    /// The ProvideRequestContext interface.
    /// </summary>
    public interface IProvideRequestContext
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether is api request.
        /// </summary>
        bool IsApiRequest { get; }

        #endregion
    }
}