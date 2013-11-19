// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BootstrapperWindsorExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The bootstrapper windsor extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Bootstrapping
{
    using Bootstrap;
    using Bootstrap.Extensions;
    using Bootstrap.Extensions.Containers;
    using Bootstrap.Windsor;

    using global::Castle.MicroKernel;

    /// <summary>
    ///     The bootstrapper windsor extensions.
    /// </summary>
    public static class BootstrapperWindsorExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The pluggable windsor.
        /// </summary>
        /// <param name="extensions">
        /// The extensions.
        /// </param>
        /// <param name="helper">
        /// The helper.
        /// </param>
        /// <param name="facilities">
        /// The facilities.
        /// </param>
        /// <returns>
        /// The <see cref="WindsorOptions"/>.
        /// </returns>
        public static WindsorOptions PluggableWindsor(
            this BootstrapperExtensions extensions, IRegistrationHelper helper, params IFacility[] facilities)
        {
            var windsorExtension = new WindsorExtension(helper, new BootstrapperContainerExtensionOptions());
            facilities.ForEach(windsorExtension.AddFacility);
            extensions.Extension(windsorExtension);
            return windsorExtension.Options;
        }

        #endregion
    }
}