// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostWasUpdated.cs" company="">
//   
// </copyright>
// <summary>
//   The post was updated.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Events.Post
{
    using Brightside.DevCom.Entities.Example;

    /// <summary>
    ///     The post was updated.
    /// </summary>
    public class PostWasUpdated
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the post.
        /// </summary>
        public Post Post { get; set; }

        #endregion
    }
}