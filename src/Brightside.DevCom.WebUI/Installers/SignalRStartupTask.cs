// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalRStartupTask.cs" company="">
//   
// </copyright>
// <summary>
//   The signal r startup task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI.Installers
{
    using System;
    using System.Web.Routing;

    using Bootstrap;
    using Bootstrap.Extensions.StartupTasks;

    using Brightside.DevCom.Infrastructure.Web.SignalR;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Microsoft.AspNet.SignalR.Infrastructure;

    using SignalR.Castle.Windsor;

    /// <summary>
    ///     The signal r startup task.
    /// </summary>
    public class SignalRStartupTask : IStartupTask
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
            var resolver = new WindsorDependencyResolver(container);

            container.Register(
                Component.For<IConnectionManager>().ImplementedBy<ConnectionManager>(),
                Component.For<IDependencyResolver>().Instance(resolver),
                Component.For<IAssemblyLocator>().ImplementedBy<PluginAssemblyLocator>());

            GlobalHost.DependencyResolver = resolver;
            RouteTable.Routes.MapHubs(new HubConfiguration { Resolver = resolver });
        }

        #endregion
    }
}