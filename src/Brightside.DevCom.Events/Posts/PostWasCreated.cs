// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostWasCreated.cs" company="">
//   
// </copyright>
// <summary>
//   The post was created.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Events.Post
{
    using Brightside.DevCom.Entities.Example;

    /// <summary>
    ///     The post was created.
    /// </summary>
    public class PostWasCreated
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the post.
        /// </summary>
        public Post Post { get; set; }

        #endregion
    }
}