// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBusinessObject.cs" company="">
//   
// </copyright>
// <summary>
//   The BusinessObject interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Entities
{
    /// <summary>
    /// The BusinessObject interface.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    /// <typeparam name="TId">
    /// </typeparam>
    public interface IBusinessObject<TEntity, TId>
        where TEntity : class, IBusinessObject<TEntity, TId>
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        TId Id { get; set; }

        #endregion
    }

    /// <summary>
    /// The BusinessObject interface.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public interface IBusinessObject<TEntity> : IBusinessObject<TEntity, int>
        where TEntity : class, IBusinessObject<TEntity>
    {
    }
}