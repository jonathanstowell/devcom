// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationNode.cs" company="">
//   
// </copyright>
// <summary>
//   The navigation node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.Plugin.Navigation.Entities
{
    using System.Collections.Generic;

    /// <summary>
    ///     The navigation node.
    /// </summary>
    public class NavigationNode
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavigationNode" /> class.
        /// </summary>
        public NavigationNode()
        {
            this.Children = new List<NavigationNode>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the action.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        ///     Gets or sets the children.
        /// </summary>
        public List<NavigationNode> Children { get; set; }

        /// <summary>
        ///     Gets or sets the controller.
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Gets a value indicating whether has legacy url.
        /// </summary>
        public bool HasLegacyUrl
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.LegacyUrl);
            }
        }

        /// <summary>
        /// Gets a value indicating whether has mvc url.
        /// </summary>
        public bool HasMvcUrl
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.Controller) && !string.IsNullOrWhiteSpace(this.Action);
            }
        }

        /// <summary>
        ///     Gets or sets the html attributes.
        /// </summary>
        public IDictionary<string, object> HtmlAttributes { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the legacy url.
        /// </summary>
        public string LegacyUrl { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        ///     Gets or sets the route values.
        /// </summary>
        public IDictionary<string, object> RouteValues { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var other = obj as NavigationNode;

            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Equals(NavigationNode other)
        {
            if (this.Name == other.Name)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}