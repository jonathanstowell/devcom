namespace Brightside.DevCom.Infrastructure.Web.SignalR
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Brightside.DevCom.Infrastructure.Plugin;
    using Brightside.DevCom.Infrastructure.Plugin.Impl;

    using Microsoft.AspNet.SignalR.Hubs;

    public class PluginAssemblyLocator : IAssemblyLocator
    {
        private readonly IPluginAssemblyProvider provider = new PluginAssemblyProvider("Plugins", "Brightside.*.dll");

        public IList<Assembly> GetAssemblies()
        {
            return provider.GetAssemblies().ToList();
        }
    }
}
