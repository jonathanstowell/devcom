// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The navigation extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Infrastructure.Web.Extensions
{
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    using Brightside.DevCom.Infrastructure.Web.Plugin.Navigation.Entities;

    /// <summary>
    /// The navigation extensions.
    /// </summary>
    public static class NavigationExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add mobile navigation.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        /// <param name="dataTheme">
        /// The data theme.
        /// </param>
        /// <param name="dataContentTheme">
        /// The data content theme.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string AddMobileNavigation(
            HtmlHelper htmlHelper, IList<NavigationNode> nodes, string dataTheme, string dataContentTheme)
        {
            var navOutput = new StringBuilder();

            foreach (NavigationNode navigationNode in nodes)
            {
                navOutput.Append(AddMobileNavigationNode(htmlHelper, navigationNode, dataTheme, dataContentTheme));
            }

            return navOutput.ToString();
        }

        /// <summary>
        /// The add mobile navigation node.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <param name="dataTheme">
        /// The data theme.
        /// </param>
        /// <param name="dataContentTheme">
        /// The data content theme.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string AddMobileNavigationNode(
            HtmlHelper htmlHelper, NavigationNode node, string dataTheme, string dataContentTheme)
        {
            var nodeOutput = new StringBuilder();

            if (node.Children.Count > 0)
            {
                nodeOutput.Append(
                    string.Format(
                        "<div data-role=\"collapsible\" data-theme=\"{0}\" data-content-theme=\"{1}\">", 
                        dataTheme, 
                        dataContentTheme));
            }

            if (node.Children.Count == 0)
            {
                nodeOutput.Append(
                    htmlHelper.ActionLink(
                        node.Name, node.Action, node.Controller, null, new { data_role = "button", rel = "external" }));
            }
            else
            {
                nodeOutput.Append(AddMobileSubMenu(htmlHelper, node.Children));
            }

            if (node.Children.Count > 0)
            {
                nodeOutput.Append("</div>");
            }

            return nodeOutput.ToString();
        }

        /// <summary>
        /// The add navigation.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        /// <param name="renderNavbar">
        /// The render navbar.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string AddNavigation(HtmlHelper htmlHelper, IList<NavigationNode> nodes, bool renderNavbar)
        {
            var navOutput = new StringBuilder();

            if (renderNavbar)
            {
                navOutput.Append("<ul class=\"nav\">");
            }

            foreach (NavigationNode navigationNode in nodes)
            {
                navOutput.Append(AddNavigationNode(htmlHelper, navigationNode));
            }

            if (renderNavbar)
            {
                navOutput.Append("</ul>");
            }

            return navOutput.ToString();
        }

        /// <summary>
        /// The add navigation node.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string AddNavigationNode(HtmlHelper htmlHelper, NavigationNode node)
        {
            var nodeOutput = new StringBuilder();

            if (node.Children.Count == 0)
            {
                nodeOutput.Append("<li>");
            }
            else
            {
                nodeOutput.Append("<li class=\"dropdown\">");
            }

            if (node.Children.Count == 0)
            {
                if (node.HasMvcUrl)
                {
                    nodeOutput.Append(
                        string.IsNullOrEmpty(node.Icon)
                            ? string.Format(
                                @"<a href=""{0}"">{1}</a>", 
                                UrlHelper.GenerateUrl(
                                    null, 
                                    node.Action, 
                                    node.Controller, 
                                    null, 
                                    null, 
                                    null, 
                                    null, 
                                    htmlHelper.RouteCollection, 
                                    htmlHelper.ViewContext.RequestContext, 
                                    true), 
                                node.Name)
                            : string.Format(
                                @"<a href=""{0}""><i class=""{1}""></i> <span>{2}</span></a>", 
                                UrlHelper.GenerateUrl(
                                    null, 
                                    node.Action, 
                                    node.Controller, 
                                    null, 
                                    null, 
                                    null, 
                                    null, 
                                    htmlHelper.RouteCollection, 
                                    htmlHelper.ViewContext.RequestContext, 
                                    true), 
                                node.Icon, 
                                node.Name));
                }
                else if (node.HasLegacyUrl)
                {
                    nodeOutput.Append(
                        string.IsNullOrEmpty(node.Icon)
                            ? string.Format(@"<a href=""{0}"">{1}</a>", node.LegacyUrl, node.Name)
                            : string.Format(
                                @"<a href=""/{0}""><i class=""{1}""></i> <span>{2}</span></a>", 
                                node.LegacyUrl, 
                                node.Icon, 
                                node.Name));
                }
            }
            else
            {
                nodeOutput.Append(
                    string.Format(
                        @"<a href=""#"" class=""dropdown-toggle"" data-toggle=""dropdown""><i class=""{0}""></i> <span>{1}</span> <b class=""caret""></b></a>", 
                        node.Icon, 
                        node.Name));
                nodeOutput.Append(AddSubMenu(htmlHelper, node.Children));
            }

            nodeOutput.Append("</li>");

            return nodeOutput.ToString();
        }

        /// <summary>
        /// The render mobile navigation.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="navigation">
        /// The navigation.
        /// </param>
        /// <param name="dataTheme">
        /// The data theme.
        /// </param>
        /// <param name="dataContentTheme">
        /// The data content theme.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString RenderMobileNavigation(
            this HtmlHelper htmlHelper, Navigation navigation, string dataTheme, string dataContentTheme)
        {
            return
                htmlHelper.Raw(AddMobileNavigation(htmlHelper, navigation.NavigationNodes, dataTheme, dataContentTheme));
        }

        /// <summary>
        /// The render navigation.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="navigation">
        /// The navigation.
        /// </param>
        /// <param name="renderNavbar">
        /// The render navbar.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString RenderNavigation(
            this HtmlHelper htmlHelper, Navigation navigation, bool renderNavbar = true)
        {
            return htmlHelper.Raw(AddNavigation(htmlHelper, navigation.NavigationNodes, renderNavbar));
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add mobile sub menu.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string AddMobileSubMenu(HtmlHelper htmlHelper, IList<NavigationNode> nodes)
        {
            var subOutput = new StringBuilder();

            foreach (NavigationNode navigationNode in nodes)
            {
                subOutput.Append(AddNavigationNode(htmlHelper, navigationNode));
            }

            return subOutput.ToString();
        }

        /// <summary>
        /// The add sub menu.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string AddSubMenu(HtmlHelper htmlHelper, IList<NavigationNode> nodes)
        {
            var subOutput = new StringBuilder();
            subOutput.Append("<ul class=\"dropdown-menu\">");
            foreach (NavigationNode navigationNode in nodes)
            {
                subOutput.Append(AddNavigationNode(htmlHelper, navigationNode));
            }

            subOutput.Append("</ul>");

            return subOutput.ToString();
        }

        #endregion
    }
}