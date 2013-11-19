// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExampleInstaller.cs" company="">
//   
// </copyright>
// <summary>
//   The example installer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Example.WebUI.Installers
{
    using System;
    using System.Web.Http.Controllers;
    using System.Web.Mvc;

    using Bootstrap.Extensions.Containers;

    using Brightside.DevCom.Example.WebUI.Validations;
    using Brightside.DevCom.Example.WebUI.Validations.Impl;
    using Brightside.DevCom.Infrastructure.Web.Conventions;
    using Brightside.DevCom.Infrastructure.Web.Plugin.Navigation;

    using Castle.Core;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using FluentValidation;

    using Microsoft.AspNet.SignalR.Hubs;

    using ReallySimpleEventing.EventHandling;

    /// <summary>
    ///     The example installer.
    /// </summary>
    public class ExampleInstaller : IBootstrapperRegistration
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
                Classes.FromThisAssembly().BasedOn<IController>().Configure(
                    component =>
                        {
                            component.Named(component.Implementation.Name);
                            component.LifeStyle.Is(LifestyleType.Transient);
                        }).WithService.Base(), 
                Classes.FromThisAssembly().BasedOn<IHttpController>().Configure(
                    component =>
                        {
                            component.Named("api." + component.Implementation.Name);
                            component.LifeStyle.Is(LifestyleType.Transient);
                        }).WithService.Base(), 
                Classes.FromThisAssembly().BasedOn<ICommand>().Unless(x => x.IsAbstract).LifestyleTransient(), 
                Classes.FromThisAssembly().BasedOn(typeof(IValidator<>)).LifestyleTransient(), 
                Component.For<IProvidePostValidators>().ImplementedBy<ProvidePostValidators>().LifeStyle.Transient, 
                Classes.FromThisAssembly()
                       .BasedOn<IHub>()
                       .Unless(x => x.IsAbstract)
                       .LifestyleTransient()
                       .WithService.FromInterface(), 
                Classes.FromThisAssembly()
                       .BasedOn(typeof(IHandle<>))
                       .Unless(x => x.IsAbstract)
                       .LifestyleTransient()
                       .WithService.FromInterface(), 
                Classes.FromThisAssembly()
                       .BasedOn<IRegisterNavigation>()
                       .Unless(x => x.IsAbstract)
                       .LifestyleSingleton()
                       .WithService.FromInterface());
        }

        /// <summary>
        ///     The reset.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void Reset()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}