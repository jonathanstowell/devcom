// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The post repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Data.Impl.Repositories
{
    using Brightside.DevCom.Entities.Example;
    using Brightside.DevCom.Infrastructure.Data;
    using Brightside.DevCom.Infrastructure.Data.Repositories.Impl;

    /// <summary>
    ///     The post repository.
    /// </summary>
    public class PostRepository : Repository<Post, DevComContext>, IPostRepository
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostRepository"/> class.
        /// </summary>
        /// <param name="contextFactory">
        /// The context Factory.
        /// </param>
        public PostRepository(IDatabaseContextFactory<DevComContext> contextFactory)
            : base(contextFactory)
        {
        }

        #endregion
    }
}