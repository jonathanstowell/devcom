// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDevComUnitOfWork.cs" company="">
//   
// </copyright>
// <summary>
//   The DevComUnitOfWork interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Data
{
    using Brightside.DevCom.Infrastructure.Data;

    /// <summary>
    ///     The DevComUnitOfWork interface.
    /// </summary>
    public interface IDevComUnitOfWork : IUnitOfWork
    {
        #region Public Properties

        /// <summary>
        /// Gets the post repository.
        /// </summary>
        IPostRepository PostRepository { get; }

        #endregion
    }
}