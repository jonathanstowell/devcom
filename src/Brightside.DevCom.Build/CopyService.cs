// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CopyService.cs" company="">
//   
// </copyright>
// <summary>
//   The copy service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Build
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Tasks;
    using Microsoft.Build.Utilities;

    /// <summary>
    ///     The copy service.
    /// </summary>
    public class CopyService : Task
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the ac.
        /// </summary>
        [Required]
        public string[] AC { get; set; }

        /// <summary>
        ///     Gets or sets the backend folder.
        /// </summary>
        public string BackendFolder { get; set; }

        /// <summary>
        ///     Gets or sets the configuration.
        /// </summary>
        [Required]
        public string Configuration { get; set; }

        /// <summary>
        ///     Gets or sets the folder.
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        ///     Gets or sets the frontend folder.
        /// </summary>
        public string FrontendFolder { get; set; }

        /// <summary>
        ///     Gets or sets the javascript plugin folder.
        /// </summary>
        public string JavascriptPluginFolder { get; set; }

        /// <summary>
        ///     Gets or sets the job folder.
        /// </summary>
        public string JobFolder { get; set; }

        /// <summary>
        ///     Gets or sets the less plugin folder.
        /// </summary>
        public string LessPluginFolder { get; set; }

        /// <summary>
        ///     Gets or sets the prefix.
        /// </summary>
        [Required]
        public string Prefix { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The execute.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public override bool Execute()
        {
            string basePath = Environment.CurrentDirectory;
            var suffixes = new[]
                               {
                                   "Configuration", "Data", "Entities", "WebUI", "Messages", "Resources", "Jobs", 
                                   "Service", "Shared", "Validations", "Models"
                               };

            foreach (string ac in this.AC)
            {
                string acPath = basePath + Path.DirectorySeparatorChar + this.Prefix + "." + ac;

                foreach (string projectSuffix in suffixes)
                {
                    string projectPath = string.Format(
                        "{0}.{1}{2}bin{3}{4}", 
                        acPath, 
                        projectSuffix, 
                        Path.DirectorySeparatorChar, 
                        Path.DirectorySeparatorChar, 
                        this.Configuration);

                    this.Log.LogCommandLine(MessageImportance.Normal, "Checking for " + projectPath);

                    ITaskItem[] files = this.BuildSourceFiles(
                        projectPath, this.Prefix + "." + ac + "." + projectSuffix + ".*");

                    if (files.Length == 0)
                    {
                        this.Log.LogWarning("Could not find {0}.", projectPath);
                        continue;
                    }

                    foreach (string destination in this.IterateDestinationsFor(ac, projectSuffix))
                    {
                        var copy = new Copy();
                        copy.BuildEngine = this.BuildEngine;
                        copy.SourceFiles = files;
                        copy.DestinationFiles = this.RebaseFilepath(projectPath, destination, files);
                        copy.Retries = 3;
                        copy.RetryDelayMilliseconds = 100;
                        copy.Execute();
                    }

                    string viewPath = projectPath.Replace("bin\\Debug", "Views");

                    ITaskItem[] views = this.BuildSourceFiles(viewPath, "*.cshtml");

                    if (views.Length == 0)
                    {
                        this.Log.LogWarning("Could not find {0}.", viewPath);
                        continue;
                    }

                    var copyViews = new Copy();
                    copyViews.BuildEngine = this.BuildEngine;
                    copyViews.SourceFiles = views;
                    copyViews.DestinationFiles = this.RebaseFilepath(viewPath, this.FrontendFolder + "\\Views", views);
                    copyViews.Retries = 3;
                    copyViews.RetryDelayMilliseconds = 100;
                    copyViews.Execute();

                    string javascriptPath = projectPath.Replace("bin\\Debug", "js");

                    ITaskItem[] javascript = this.BuildSourceFiles(javascriptPath, "*.js");

                    if (javascript.Length == 0)
                    {
                        this.Log.LogWarning("Could not find {0}.", javascriptPath);
                        continue;
                    }

                    var copyJavascript = new Copy();
                    copyJavascript.BuildEngine = this.BuildEngine;
                    copyJavascript.SourceFiles = javascript;
                    copyJavascript.DestinationFiles = this.RebaseFilepath(
                        javascriptPath, this.JavascriptPluginFolder, javascript);
                    copyJavascript.Retries = 3;
                    copyJavascript.RetryDelayMilliseconds = 100;
                    copyJavascript.Execute();

                    string lessPath = projectPath.Replace("bin\\Debug", "css");

                    ITaskItem[] less = this.BuildSourceFiles(lessPath, "*.less");

                    if (less.Length == 0)
                    {
                        this.Log.LogWarning("Could not find {0}.", lessPath);
                        continue;
                    }

                    var copyLess = new Copy();
                    copyLess.BuildEngine = this.BuildEngine;
                    copyLess.SourceFiles = less;
                    copyLess.DestinationFiles = this.RebaseFilepath(lessPath, this.LessPluginFolder, less);
                    copyLess.Retries = 3;
                    copyLess.RetryDelayMilliseconds = 100;
                    copyLess.Execute();
                }
            }

            this.CombinePluginLessFiles();

            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The build source files.
        /// </summary>
        /// <param name="directory">
        /// The directory.
        /// </param>
        /// <param name="mask">
        /// The mask.
        /// </param>
        /// <returns>
        /// The <see cref="ITaskItem[]"/>.
        /// </returns>
        private ITaskItem[] BuildSourceFiles(string directory, string mask)
        {
            var ret = new List<ITaskItem>();

            if (!Directory.Exists(directory))
            {
                return new ITaskItem[] { };
            }

            var dir = new DirectoryInfo(directory);

            foreach (FileInfo file in dir.EnumerateFiles(mask, SearchOption.AllDirectories))
            {
                ret.Add(new TaskItem(file.FullName));
            }

            return ret.ToArray();
        }

        /// <summary>
        ///     The combine plugin less files.
        /// </summary>
        private void CombinePluginLessFiles()
        {
            string path = Path.Combine(this.LessPluginFolder, "registration.less");

            using (StreamWriter combinedStyle = File.CreateText(path))
            {
                string[] pluginStyles = Directory.GetFiles(this.LessPluginFolder, "*.less");

                foreach (string style in pluginStyles)
                {
                    var info = new FileInfo(style);

                    if (info.Name == "registration.less")
                    {
                        continue;
                    }

                    combinedStyle.Write("@import " + info.Name + "\";\n");
                }

                combinedStyle.Close();
            }
        }

        /// <summary>
        /// The iterate destinations for.
        /// </summary>
        /// <param name="ac">
        /// The ac.
        /// </param>
        /// <param name="projectSuffix">
        /// The project suffix.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        private IEnumerable<string> IterateDestinationsFor(string ac, string projectSuffix)
        {
            switch (projectSuffix)
            {
                case "Configuration":
                case "Data.Abstract":
                case "Entities":
                case "Resources":
                case "Service":
                case "Shared":
                case "Validations":
                    if (!string.IsNullOrEmpty(this.FrontendFolder))
                    {
                        if (string.IsNullOrWhiteSpace(this.Folder))
                        {
                            yield return this.FrontendFolder;
                        }

                        yield return this.FrontendFolder + "\\" + this.Folder;
                    }

                    if (!string.IsNullOrEmpty(this.BackendFolder))
                    {
                        yield return this.BackendFolder;
                    }

                    if (!string.IsNullOrEmpty(this.JobFolder))
                    {
                        yield return this.JobFolder;
                    }

                    break;
                case "Messages":
                    if (!string.IsNullOrEmpty(this.FrontendFolder))
                    {
                        if (string.IsNullOrWhiteSpace(this.Folder))
                        {
                            yield return this.FrontendFolder;
                        }

                        yield return this.FrontendFolder + "\\" + this.Folder;
                    }

                    if (!string.IsNullOrEmpty(this.BackendFolder))
                    {
                        yield return this.BackendFolder;
                    }

                    break;
                case "Models":
                    if (!string.IsNullOrEmpty(this.FrontendFolder))
                    {
                        if (string.IsNullOrWhiteSpace(this.Folder))
                        {
                            yield return this.FrontendFolder;
                        }

                        yield return this.FrontendFolder + "\\" + this.Folder;
                    }

                    break;
                case "WebUI":
                    if (!string.IsNullOrEmpty(this.FrontendFolder))
                    {
                        if (string.IsNullOrWhiteSpace(this.Folder))
                        {
                            yield return this.FrontendFolder;
                        }

                        yield return this.FrontendFolder + "\\" + this.Folder;
                    }

                    break;
                case "Jobs":
                    if (!string.IsNullOrEmpty(this.JobFolder))
                    {
                        yield return this.JobFolder;
                    }

                    break;
                default:
                    throw new Exception("Unknown suffix " + projectSuffix);
            }
        }

        /// <summary>
        /// The rebase filepath.
        /// </summary>
        /// <param name="sourcePrefix">
        /// The source prefix.
        /// </param>
        /// <param name="destinationPrefix">
        /// The destination prefix.
        /// </param>
        /// <param name="files">
        /// The files.
        /// </param>
        /// <returns>
        /// The <see cref="ITaskItem[]"/>.
        /// </returns>
        private ITaskItem[] RebaseFilepath(string sourcePrefix, string destinationPrefix, IEnumerable<ITaskItem> files)
        {
            int length = sourcePrefix.Length + 1; // escaped slash
            var ret = new List<TaskItem>();

            foreach (ITaskItem file in files)
            {
                string name = Path.Combine(destinationPrefix, file.ItemSpec.Substring(length));
                ret.Add(new TaskItem(name));
            }

            return ret.ToArray();
        }

        #endregion
    }
}