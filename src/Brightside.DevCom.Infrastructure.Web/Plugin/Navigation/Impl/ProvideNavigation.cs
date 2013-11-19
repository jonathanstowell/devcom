// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProvideNavigation.cs" company="">
//   
// </copyright>
// <summary>
//   The provide navigation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.Plugin.Navigation.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Brightside.DevCom.Infrastructure.Web.Plugin.Navigation.Entities;

    /// <summary>
    ///     The provide navigation.
    /// </summary>
    public class ProvideNavigation : IProvideNavigationTree
    {
        #region Fields

        /// <summary>
        ///     The navigations.
        /// </summary>
        private readonly IEnumerable<IRegisterNavigation> navigations;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProvideNavigation"/> class.
        /// </summary>
        /// <param name="navigations">
        /// The navigations.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public ProvideNavigation(IEnumerable<IRegisterNavigation> navigations)
        {
            if (navigations == null)
            {
                throw new ArgumentNullException("navigations");
            }

            this.navigations = navigations;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get navigation.
        /// </summary>
        /// <returns>
        ///     The <see cref="Navigation" />.
        /// </returns>
        public Navigation GetNavigation()
        {
            var result = new Navigation();

            foreach (IRegisterNavigation nav in this.navigations.OrderBy(x => x.Path.Name))
            {
                result.Add(nav);
            }

            return result;
        }

        #endregion
    }
}