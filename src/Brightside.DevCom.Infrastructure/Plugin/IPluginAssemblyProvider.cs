// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPluginAssemblyProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The PluginAssemblyProvider interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Plugin
{
    using System.Collections.Generic;
    using System.Reflection;

    using Bootstrap.Extensions.Containers;

    /// <summary>
    ///     The PluginAssemblyProvider interface.
    /// </summary>
    public interface IPluginAssemblyProvider : IBootstrapperAssemblyProvider
    {
        #region Public Properties

        /// <summary>
        ///     Gets the plugin assemblies by full name.
        /// </summary>
        IDictionary<string, Assembly> PluginAssembliesByFullName { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get assembly.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="Assembly"/>.
        /// </returns>
        Assembly GetAssembly(string name);

        #endregion
    }
}