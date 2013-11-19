// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScriptExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The script extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    using Yahoo.Yui.Compressor;

    /// <summary>
    ///     The script extensions.
    /// </summary>
    public static class ScriptExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add script block.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="script">
        /// The script.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public static void AddScriptBlock(this HtmlHelper htmlHelper, string script)
        {
            var scriptGroup = htmlHelper.ViewContext.HttpContext.Items[ScriptContext.ScriptContextItem] as ScriptContext;

            if (scriptGroup == null)
            {
                throw new InvalidOperationException(
                    "cannot add a script block without a script context. Call Html.BeginScriptContext() beforehand.");
            }

            scriptGroup.ScriptBlocks.Add(script);
        }

        /// <summary>
        /// The add script file.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public static void AddScriptFile(this HtmlHelper htmlHelper, string path)
        {
            var scriptGroup = htmlHelper.ViewContext.HttpContext.Items[ScriptContext.ScriptContextItem] as ScriptContext;

            if (scriptGroup == null)
            {
                throw new InvalidOperationException(
                    "cannot add a script file without a script context. Call Html.BeginScriptContext() beforehand.");
            }

            scriptGroup.ScriptFiles.Add(path);
        }

        /// <summary>
        /// The begin script context.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <returns>
        /// The <see cref="ScriptContext"/>.
        /// </returns>
        public static ScriptContext BeginScriptContext(this HtmlHelper htmlHelper)
        {
            var scriptContext = new ScriptContext(htmlHelper.ViewContext.HttpContext);
            htmlHelper.ViewContext.HttpContext.Items[ScriptContext.ScriptContextItem] = scriptContext;
            return scriptContext;
        }

        /// <summary>
        /// The end script context.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        public static void EndScriptContext(this HtmlHelper htmlHelper)
        {
            IDictionary items = htmlHelper.ViewContext.HttpContext.Items;
            var scriptContext = items[ScriptContext.ScriptContextItem] as ScriptContext;

            if (scriptContext != null)
            {
                scriptContext.Dispose();
            }
        }

        /// <summary>
        /// The render scripts.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="compress">
        /// The compress.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString RenderScripts(this HtmlHelper htmlHelper, bool compress = false)
        {
            var scriptContexts =
                htmlHelper.ViewContext.HttpContext.Items[ScriptContext.ScriptContextItems] as Stack<ScriptContext>;

            if (scriptContexts != null)
            {
                int count = scriptContexts.Count;
                var builder = new StringBuilder();
                var rawScriptBuilder = new StringBuilder();
                var script = new List<string>();
                var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection);

                for (int i = 0; i < count; i++)
                {
                    ScriptContext scriptContext = scriptContexts.Pop();

                    foreach (string scriptFile in scriptContext.ScriptFiles)
                    {
                        builder.AppendLine(
                            "<script type='text/javascript' src='" + urlHelper.Content(scriptFile) + "'></script>");
                    }

                    script.AddRange(scriptContext.ScriptBlocks);

                    // render out all the scripts in one block on the last loop iteration
                    if (i == count - 1)
                    {
                        foreach (string s in script)
                        {
                            rawScriptBuilder.AppendLine(s);
                        }
                    }
                }

                string outputScript = "<script type='text/javascript'>";
                outputScript += compress
                                    ? JavaScriptCompressor.Compress(rawScriptBuilder.ToString())
                                    : rawScriptBuilder.ToString();
                outputScript += "</script>";

                builder.Append(outputScript);

                return new MvcHtmlString(builder.ToString());
            }

            return MvcHtmlString.Empty;
        }

        #endregion
    }

    /// <summary>
    ///     The script context.
    /// </summary>
    public class ScriptContext : IDisposable
    {
        #region Constants

        /// <summary>
        ///     The script context item.
        /// </summary>
        internal const string ScriptContextItem = "ScriptContext";

        /// <summary>
        ///     The script context items.
        /// </summary>
        internal const string ScriptContextItems = "ScriptContexts";

        #endregion

        #region Fields

        /// <summary>
        ///     The _http context.
        /// </summary>
        private readonly HttpContextBase _httpContext;

        /// <summary>
        ///     The _script blocks.
        /// </summary>
        private readonly IList<string> _scriptBlocks = new List<string>();

        /// <summary>
        ///     The _script files.
        /// </summary>
        private readonly HashSet<string> _scriptFiles = new HashSet<string>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptContext"/> class.
        /// </summary>
        /// <param name="httpContext">
        /// The http context.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public ScriptContext(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            this._httpContext = httpContext;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the script blocks.
        /// </summary>
        public IList<string> ScriptBlocks
        {
            get
            {
                return this._scriptBlocks;
            }
        }

        /// <summary>
        ///     Gets the script files.
        /// </summary>
        public HashSet<string> ScriptFiles
        {
            get
            {
                return this._scriptFiles;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The dispose.
        /// </summary>
        public void Dispose()
        {
            IDictionary items = this._httpContext.Items;
            Stack<ScriptContext> scriptContexts = items[ScriptContextItems] as Stack<ScriptContext>
                                                  ?? new Stack<ScriptContext>();

            // remove any script files already the same as the ones we're about to add
            foreach (ScriptContext scriptContext in scriptContexts)
            {
                scriptContext.ScriptFiles.ExceptWith(this.ScriptFiles);
            }

            scriptContexts.Push(this);

            items[ScriptContextItems] = scriptContexts;
        }

        #endregion
    }
}