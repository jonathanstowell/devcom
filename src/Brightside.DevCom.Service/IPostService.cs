// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPostService.cs" company="">
//   
// </copyright>
// <summary>
//   The PostService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Service
{
    using System.Collections.Generic;

    using Brightside.DevCom.Entities.Posts;

    /// <summary>
    ///     The PostService interface.
    /// </summary>
    public interface IPostService
    {
        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="post">
        /// The post.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Create(Post post);

        /// <summary>
        ///     The get.
        /// </summary>
        /// <returns>
        ///     The <see cref="IList" />.
        /// </returns>
        IList<Post> Get();

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Post"/>.
        /// </returns>
        Post GetById(int id);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="post">
        /// The post.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Update(Post post);

        #endregion
    }
}