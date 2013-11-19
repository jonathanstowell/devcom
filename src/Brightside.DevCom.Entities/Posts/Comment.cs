// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Comment.cs" company="">
//   
// </copyright>
// <summary>
//   The comment.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Entities.Example
{
    using Brightside.DevCom.Infrastructure.Entities;
    using Brightside.DevCom.Infrastructure.Entities.Impl;

    using Newtonsoft.Json;

    /// <summary>
    ///     The comment.
    /// </summary>
    public class Comment : BusinessObject<Comment>, IBusinessObject<Comment>
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the author.
        /// </summary>
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        /// <summary>
        ///     Gets or sets the content.
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        /// <summary>
        ///     Gets or sets the post.
        /// </summary>
        [JsonProperty(PropertyName = "post")]
        public virtual Post Post { get; set; }

        #endregion
    }
}