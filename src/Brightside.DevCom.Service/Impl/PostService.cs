// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostService.cs" company="">
//   
// </copyright>
// <summary>
//   The post service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Service.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Brightside.DevCom.Data;
    using Brightside.DevCom.Entities.Example;

    /// <summary>
    ///     The post service.
    /// </summary>
    public class PostService : IPostService
    {
        #region Fields

        /// <summary>
        ///     The unit of work.
        /// </summary>
        private readonly IDevComUnitOfWork unitOfWork;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostService"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        public PostService(IDevComUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #endregion

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
        public bool Create(Post post)
        {
            try
            {
                this.unitOfWork.PostRepository.Create(post);
                this.unitOfWork.Save();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     The get.
        /// </summary>
        /// <returns>
        ///     The <see cref="IList" />.
        /// </returns>
        public IList<Post> Get()
        {
            return this.unitOfWork.PostRepository.Get().ToList();
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Post"/>.
        /// </returns>
        public Post GetById(int id)
        {
            return this.unitOfWork.PostRepository.GetById(id);
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="post">
        /// The post.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Update(Post post)
        {
            try
            {
                this.unitOfWork.PostRepository.Update(post);
                this.unitOfWork.Save();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}