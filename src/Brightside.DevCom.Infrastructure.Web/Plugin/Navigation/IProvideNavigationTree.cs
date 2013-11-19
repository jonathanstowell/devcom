// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProvideNavigationTree.cs" company="">
//   
// </copyright>
// <summary>
//   The ProvideNavigationTree interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.Plugin.Navigation
{
    using Brightside.DevCom.Infrastructure.Web.Plugin.Navigation.Entities;

    /// <summary>
    ///     The ProvideNavigationTree interface.
    /// </summary>
    public interface IProvideNavigationTree
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The get navigation.
        /// </summary>
        /// <returns>
        ///     The <see cref="Navigation" />.
        /// </returns>
        Navigation GetNavigation();

        #endregion
    }
}