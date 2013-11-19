﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterNavigation.cs" company="">
//   
// </copyright>
// <summary>
//   The register navigation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Posts.WebUI.Installers
{
    using System.Collections.Generic;

    using Brightside.DevCom.Infrastructure.Web.Plugin.Navigation;
    using Brightside.DevCom.Infrastructure.Web.Plugin.Navigation.Entities;

    /// <summary>
    /// The register navigation.
    /// </summary>
    public class RegisterNavigation : IRegisterNavigation
    {
        #region Public Properties

        /// <summary>
        /// Gets the path.
        /// </summary>
        public NavigationNode Path
        {
            get
            {
                return new NavigationNode
                           {
                               Name = "Posts", 
                               Icon = "icon-file-text", 
                               Action = "Index", 
                               Controller = "Post"
                           };
            }
        }

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        public string[] Permissions
        {
            get
            {
                return new List<string>().ToArray();
            }
        }

        /// <summary>
        /// Gets a value indicating whether require all permissions.
        /// </summary>
        public bool RequireAllPermissions
        {
            get
            {
                return false;
            }
        }

        #endregion
    }
}