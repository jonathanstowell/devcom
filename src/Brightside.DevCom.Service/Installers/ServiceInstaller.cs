// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceInstaller.cs" company="">
//   
// </copyright>
// <summary>
//   The service installer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Service.Installers
{
    using Bootstrap.Extensions.Containers;

    using Brightside.DevCom.Service.Impl;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    /// <summary>
    ///     The service installer.
    /// </summary>
    public class ServiceInstaller : IBootstrapperRegistration
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="containerExtension">
        /// The container extension.
        /// </param>
        public void Register(IBootstrapperContainerExtension containerExtension)
        {
            var container = (IWindsorContainer)containerExtension.Container;

            container.Register(Component.For<IPostService>().ImplementedBy<PostService>().LifeStyle.PerWebRequest);
        }

        #endregion
    }
}