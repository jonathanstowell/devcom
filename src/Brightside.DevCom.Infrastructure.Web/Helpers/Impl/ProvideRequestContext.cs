// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProvideRequestContext.cs" company="">
//   
// </copyright>
// <summary>
//   The provide request context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Infrastructure.Web.Helpers.Impl
{
    using System.Web;

    /// <summary>
    /// The provide request context.
    /// </summary>
    public class ProvideRequestContext : IProvideRequestContext
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether is api request.
        /// </summary>
        public bool IsApiRequest
        {
            get
            {
                return HttpContext.Current.Request.Url.ToString().Contains("api");
            }
        }

        #endregion
    }
}