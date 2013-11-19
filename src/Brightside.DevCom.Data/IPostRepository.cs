// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPostRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The PostRepository interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Data
{
    using Brightside.DevCom.Entities.Example;
    using Brightside.DevCom.Infrastructure.Data.Repositories;

    /// <summary>
    ///     The PostRepository interface.
    /// </summary>
    public interface IPostRepository : IRepository<Post>
    {
    }
}