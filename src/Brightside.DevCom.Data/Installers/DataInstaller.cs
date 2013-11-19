// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataInstaller.cs" company="">
//   
// </copyright>
// <summary>
//   The data installer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Data.Installers
{
    using Bootstrap.Extensions.Containers;

    using Brightside.DevCom.Data.Impl;
    using Brightside.DevCom.Data.Impl.Repositories;
    using Brightside.DevCom.Infrastructure.Data;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    /// <summary>
    ///     The data installer.
    /// </summary>
    public class DataInstaller : IBootstrapperRegistration
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

            container.Register(
                Component.For<IPostRepository>().ImplementedBy<PostRepository>().LifeStyle.PerWebRequest, 
                Component.For<IDevComUnitOfWork>().ImplementedBy<DevComUnitOfWork>().LifeStyle.PerWebRequest, 
                Component.For<IDatabaseContextFactory<DevComContext>>()
                         .ImplementedBy<DevComDatabaseContextFactory>()
                         .LifeStyle.PerWebRequest);
        }

        #endregion
    }
}