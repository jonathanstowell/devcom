// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrecompiledView.cs" company="">
//   
// </copyright>
// <summary>
//   The start page lookup delegate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.ViewEngines.Precompiled
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Mvc;
    using System.Web.WebPages;

    /// <summary>
    ///     The start page lookup delegate.
    /// </summary>
    /// <param name="page">
    ///     The page.
    /// </param>
    /// <param name="fileName">
    ///     The file name.
    /// </param>
    /// <param name="supportedExtensions">
    ///     The supported extensions.
    /// </param>
    internal delegate WebPageRenderingBase StartPageLookupDelegate(
        WebPageRenderingBase page, string fileName, IEnumerable<string> supportedExtensions);

    /// <summary>
    ///     The PrecompiledView interface.
    /// </summary>
    public interface IPrecompiledView
    {
    }

    /// <summary>
    ///     The precompiled view.
    /// </summary>
    public class PrecompiledView : IView
    {
        #region Fields

        /// <summary>
        ///     The type.
        /// </summary>
        private readonly WebViewPage type;

        /// <summary>
        ///     The virtual path.
        /// </summary>
        private readonly string virtualPath;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecompiledView"/> class.
        /// </summary>
        /// <param name="virtualPath">
        /// The virtual path.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="runViewStartPages">
        /// The run view start pages.
        /// </param>
        /// <param name="fileExtension">
        /// The file extension.
        /// </param>
        public PrecompiledView(
            string virtualPath, WebViewPage type, bool runViewStartPages, IEnumerable<string> fileExtension)
        {
            this.type = type;
            this.virtualPath = virtualPath;
            this.RunViewStartPages = runViewStartPages;
            this.ViewStartFileExtensions = fileExtension;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether run view start pages.
        /// </summary>
        public bool RunViewStartPages { get; private set; }

        /// <summary>
        ///     Gets the view start file extensions.
        /// </summary>
        public IEnumerable<string> ViewStartFileExtensions { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="viewContext">
        /// The view context.
        /// </param>
        /// <param name="writer">
        /// The writer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            WebViewPage webViewPage = this.type;
            if (webViewPage == null)
            {
                throw new InvalidOperationException("Invalid view type");
            }

            webViewPage.VirtualPath = this.virtualPath;
            webViewPage.ViewContext = viewContext;
            webViewPage.ViewData = viewContext.ViewData;
            webViewPage.InitHelpers();
            WebPageRenderingBase startPage = null;
            if (this.RunViewStartPages)
            {
                startPage = StartPage.GetStartPage(webViewPage, "_ViewStart", this.ViewStartFileExtensions);
            }

            var pageContext = new WebPageContext(viewContext.HttpContext, webViewPage, null);
            webViewPage.ExecutePageHierarchy(pageContext, writer, startPage);
        }

        #endregion
    }
}