// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterNavigation.cs" company="">
//   
// </copyright>
// <summary>
//   The register navigation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Knockout.WebUI.Installers
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
                               Name = "Knockout",
                               Icon = "icon-cogs",
                               Children = new List<NavigationNode>
                                {
                                    new NavigationNode
                                    {
                                        Name = "Observables",
                                        Action = "Index",
                                        Controller = "Knockout"
                                    },
                                    new NavigationNode
                                    {
                                        Name = "Computed",
                                        Action = "Computed",
                                        Controller = "Knockout"
                                    },
                                    new NavigationNode
                                    {
                                        Name = "Subscriptions",
                                        Action = "Subscriptions",
                                        Controller = "Knockout"
                                    }
                                }
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