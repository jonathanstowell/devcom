// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPagedResult.cs" company="">
//   
// </copyright>
// <summary>
//   The PagedResult interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Infrastructure.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// The PagedResult interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IPagedResult<T>
        where T : class
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether has next page.
        /// </summary>
        bool HasNextPage { get; }

        /// <summary>
        /// Gets a value indicating whether has previous page.
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Gets a value indicating whether is first page.
        /// </summary>
        bool IsFirstPage { get; }

        /// <summary>
        /// Gets a value indicating whether is last page.
        /// </summary>
        bool IsLastPage { get; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Gets the page count.
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Gets the page number.
        /// </summary>
        int PageNumber { get; }

        /// <summary>
        /// Gets the page size.
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Gets the total item count.
        /// </summary>
        int TotalItemCount { get; }

        #endregion
    }
}