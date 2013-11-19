// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDatabaseContextFactory.cs" company="">
//   
// </copyright>
// <summary>
//   The DatabaseContextFactory interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Data
{
    using System.Data.Entity;

    /// <summary>
    /// The DatabaseContextFactory interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IDatabaseContextFactory<T>
        where T : DbContext
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The get context.
        /// </summary>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        T GetContext();

        #endregion
    }
}