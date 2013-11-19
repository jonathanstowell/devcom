// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Navigation.cs" company="">
//   
// </copyright>
// <summary>
//   The navigation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.Plugin.Navigation.Entities
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     The navigation.
    /// </summary>
    public class Navigation
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Navigation" /> class.
        /// </summary>
        public Navigation()
        {
            this.NavigationNodes = new List<NavigationNode>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the navigation nodes.
        /// </summary>
        public IList<NavigationNode> NavigationNodes { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="navigation">
        /// The navigation.
        /// </param>
        public void Add(IRegisterNavigation navigation)
        {
            this.AddInternal(navigation.Path);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add internal.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        private void AddInternal(NavigationNode path)
        {
            this.AddRecursive(this.NavigationNodes, path);
        }

        /// <summary>
        /// The add recursive.
        /// </summary>
        /// <param name="navigationNodes">
        /// The navigation nodes.
        /// </param>
        /// <param name="toAdd">
        /// The to add.
        /// </param>
        private void AddRecursive(IList<NavigationNode> navigationNodes, NavigationNode toAdd)
        {
            if (navigationNodes.Contains(toAdd))
            {
                foreach (NavigationNode node in toAdd.Children)
                {
                    this.AddRecursive(navigationNodes.Single(x => x.Name == toAdd.Name).Children, node);
                }
            }
            else
            {
                navigationNodes.Add(toAdd);
            }
        }

        #endregion
    }
}