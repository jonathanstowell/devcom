// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebUIStartupTask.cs" company="">
//   
// </copyright>
// <summary>
//   The web ui startup task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI.Installers
{
    using System;
    using System.Web.Http.Controllers;
    using System.Web.Mvc;

    using Bootstrap;
    using Bootstrap.Extensions.StartupTasks;

    using Brightside.DevCom.Infrastructure.Web.Conventions;
    using Brightside.DevCom.Infrastructure.Web.Helpers;
    using Brightside.DevCom.Infrastructure.Web.Helpers.Impl;
    using Brightside.DevCom.Infrastructure.Web.Plugin.Navigation;
    using Brightside.DevCom.Infrastructure.Web.Plugin.Navigation.Impl;

    using Castle.Core;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using FluentValidation;

    using Microsoft.AspNet.SignalR.Hubs;

    using ReallySimpleEventing;
    using ReallySimpleEventing.EventHandling;

    /// <summary>
    ///     The web ui startup task.
    /// </summary>
    public class WebUIStartupTask : IStartupTask
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The reset.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void Reset()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     The run.
        /// </summary>
        public void Run()
        {
            var container = (IWindsorContainer)Bootstrapper.Container;

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
                       .WithService.FromInterface(), 
                Component.For<IEventStream>().UsingFactoryMethod(ReallySimpleEventing.CreateEventStream), 
                Component.For<IProvideNavigationTree>().ImplementedBy<ProvideNavigation>().LifestyleSingleton(), 
                Component.For<IProvideRequestContext>().ImplementedBy<ProvideRequestContext>().LifestyleSingleton());
        }

        #endregion
    }
}