// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindsorPrecompiledViewEngine.cs" company="">
//   
// </copyright>
// <summary>
//   The windsor precompiled view engine.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.ViewEngines.Precompiled
{
    using System;
    using System.Web.Mvc;

    using global::Castle.Windsor;

    /// <summary>
    ///     The windsor precompiled view engine.
    /// </summary>
    public class WindsorPrecompiledViewEngine : VirtualPathProviderViewEngine
    {
        #region Fields

        /// <summary>
        ///     The container.
        /// </summary>
        private readonly IWindsorContainer container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorPrecompiledViewEngine"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public WindsorPrecompiledViewEngine(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.AreaViewLocationFormats = new[]
                                               {
                                                   "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/{1}/{0}.vbhtml", 
                                                   "~/Areas/{2}/Views/Shared/{0}.cshtml", 
                                                   "~/Areas/{2}/Views/Shared/{0}.vbhtml"
                                               };

            this.AreaMasterLocationFormats = new[]
                                                 {
                                                     "~/Areas/{2}/Views/{1}/{0}.cshtml", 
                                                     "~/Areas/{2}/Views/{1}/{0}.vbhtml", 
                                                     "~/Areas/{2}/Views/Shared/{0}.cshtml", 
                                                     "~/Areas/{2}/Views/Shared/{0}.vbhtml"
                                                 };

            this.AreaPartialViewLocationFormats = new[]
                                                      {
                                                          "~/Areas/{2}/Views/{1}/{0}.cshtml", 
                                                          "~/Areas/{2}/Views/{1}/{0}.vbhtml", 
                                                          "~/Areas/{2}/Views/Shared/{0}.cshtml", 
                                                          "~/Areas/{2}/Views/Shared/{0}.vbhtml"
                                                      };

            this.ViewLocationFormats = new[]
                                           {
                                               "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml", 
                                               "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml"
                                           };

            this.MasterLocationFormats = new[]
                                             {
                                                 "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml", 
                                                 "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml"
                                             };

            this.PartialViewLocationFormats = new[]
                                                  {
                                                      "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml", 
                                                      "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml"
                                                  };

            this.FileExtensions = new[] { "cshtml" };

            this.container = container;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The exists.
        /// </summary>
        /// <param name="virtualPath">
        /// The virtual path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Exists(string virtualPath)
        {
            WebViewPage page;

            try
            {
                page = this.container.Kernel.Resolve<WebViewPage>(this.GetKey(virtualPath));
            }
            catch (Exception)
            {
                page = null;
            }

            return page != null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The create partial view.
        /// </summary>
        /// <param name="controllerContext">
        /// The controller context.
        /// </param>
        /// <param name="partialPath">
        /// The partial path.
        /// </param>
        /// <returns>
        /// The <see cref="IView"/>.
        /// </returns>
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return this.GetView(controllerContext, partialPath, false);
        }

        /// <summary>
        /// The create view.
        /// </summary>
        /// <param name="controllerContext">
        /// The controller context.
        /// </param>
        /// <param name="viewPath">
        /// The view path.
        /// </param>
        /// <param name="masterPath">
        /// The master path.
        /// </param>
        /// <returns>
        /// The <see cref="IView"/>.
        /// </returns>
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return this.GetView(controllerContext, viewPath, true);
        }

        /// <summary>
        /// The file exists.
        /// </summary>
        /// <param name="controllerContext">
        /// The controller context.
        /// </param>
        /// <param name="virtualPath">
        /// The virtual path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return this.Exists(virtualPath);
        }

        // Attempt to find page from container. If you do great return it. If not and the context is mobile, try and resolve a normal webpage to fall back on.

        /// <summary>
        /// The get key.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetKey(string path)
        {
            string s = path.TrimStart("~/Views/".ToCharArray()).TrimEnd("cshtml".ToCharArray()).Replace('/', '.');
            s = s.Remove(s.Length - 1, 1);
            return s;
        }

        /// <summary>
        /// The get view.
        /// </summary>
        /// <param name="controllerContext">
        /// The controller context.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="runViewStart">
        /// The run view start.
        /// </param>
        /// <returns>
        /// The <see cref="PrecompiledView"/>.
        /// </returns>
        private PrecompiledView GetView(ControllerContext controllerContext, string path, bool runViewStart)
        {
            WebViewPage type;

            try
            {
                type = this.container.Kernel.Resolve<WebViewPage>(this.GetKey(path));
            }
            catch (Exception)
            {
                type = null;
            }

            return type != null ? new PrecompiledView(path, type, runViewStart, this.FileExtensions) : null;
        }

        #endregion
    }
}