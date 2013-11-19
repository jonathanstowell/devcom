// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginAssemblyProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The plugin assembly provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Plugin.Impl
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Bootstrap.Extensions.Containers;

    /// <summary>
    ///     The plugin assembly provider.
    /// </summary>
    public class PluginAssemblyProvider : LoadedAssemblyProvider, IPluginAssemblyProvider
    {
        #region Fields

        /// <summary>
        ///     The path.
        /// </summary>
        private readonly string path;

        /// <summary>
        ///     The patterns.
        /// </summary>
        private readonly string[] patterns;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginAssemblyProvider"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="patterns">
        /// The patterns.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public PluginAssemblyProvider(string path, params string[] patterns)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            this.path = path;
            this.patterns = patterns;
            pluginAssembliesByFullName = new Dictionary<string, Assembly>();
            this.LoadPluginAssemblies();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the plugin assemblies by full name.
        /// </summary>
        public IDictionary<string, Assembly> PluginAssembliesByFullName
        {
            get
            {
                return pluginAssembliesByFullName;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the plugin assemblies by full name.
        /// </summary>
        private static Dictionary<string, Assembly> pluginAssembliesByFullName { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get assemblies.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public IEnumerable<Assembly> GetAssemblies()
        {
            return
                AppDomain.CurrentDomain.GetAssemblies().Union(pluginAssembliesByFullName.Select(x => x.Value).ToArray());
        }

        /// <summary>
        /// The get assembly.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="Assembly"/>.
        /// </returns>
        public Assembly GetAssembly(string name)
        {
            Assembly resolvedAssembly;
            if (this.TryGetAssembly(name, out resolvedAssembly))
            {
                return resolvedAssembly;
            }

            return null;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The load plugin assemblies.
        /// </summary>
        private void LoadPluginAssemblies()
        {
            string scanPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.path);
            var directory = new DirectoryInfo(scanPath);

            var files = new HashSet<FileInfo>();

            try
            {
                foreach (string pattern in this.patterns)
                {
                    FileInfo[] found = directory.GetFiles(pattern, SearchOption.AllDirectories);
                    files.UnionWith(found);
                }
            }
            catch (Exception)
            {
            }

            var ret = new List<Assembly>();

            foreach (FileInfo file in files)
            {
                Assembly assembly = Assembly.LoadFrom(file.FullName);
                assembly.GetTypes();
                ret.Add(assembly);
                this.RegisterAssembly(assembly.FullName, assembly);
            }
        }

        /// <summary>
        /// The register assembly.
        /// </summary>
        /// <param name="assemblyName">
        /// The assembly name.
        /// </param>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        private void RegisterAssembly(string assemblyName, Assembly assembly)
        {
            int comma = assemblyName.IndexOf(',');
            pluginAssembliesByFullName[assemblyName] = assembly;
            pluginAssembliesByFullName[comma == -1 ? assemblyName : assemblyName.Substring(0, comma)] = assembly;
        }

        /// <summary>
        /// The try get assembly.
        /// </summary>
        /// <param name="assemblyName">
        /// The assembly name.
        /// </param>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool TryGetAssembly(string assemblyName, out Assembly assembly)
        {
            return this.PluginAssembliesByFullName.TryGetValue(assemblyName, out assembly);
        }

        #endregion
    }
}