// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Post.cs" company="">
//   
// </copyright>
// <summary>
//   The post.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Entities.Example
{
    using System.Collections.Generic;

    using Brightside.DevCom.Infrastructure.Entities;
    using Brightside.DevCom.Infrastructure.Entities.Impl;

    using Newtonsoft.Json;

    /// <summary>
    ///     The post.
    /// </summary>
    public class Post : BusinessObject<Post>, IBusinessObject<Post>
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Post" /> class.
        /// </summary>
        public Post()
        {
            this.Comments = new List<Comment>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the author.
        /// </summary>
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        /// <summary>
        ///     Gets or sets the comments.
        /// </summary>
        [JsonProperty(PropertyName = "comments")]
        public virtual IList<Comment> Comments { get; set; }

        /// <summary>
        ///     Gets or sets the content.
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        #endregion
    }
}