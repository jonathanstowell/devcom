// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PagedExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The paged extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Infrastructure.Web.Extensions
{
    using System;
    using System.Linq;

    using Brightside.DevCom.Infrastructure.Entities;
    using Brightside.DevCom.Infrastructure.Entities.Impl;

    /// <summary>
    /// The paged extensions.
    /// </summary>
    public static class PagedExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The map paged.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="T1">
        /// </typeparam>
        /// <returns>
        /// The <see cref="PagedResult"/>.
        /// </returns>
        public static PagedResult<T> MapPaged<T, T1>(this IPagedResult<T1> item, Func<T1, T> map) where T : class
            where T1 : class
        {
            return new PagedResult<T>(
                item.Items.Select(map).AsEnumerable(), item.TotalItemCount, item.PageNumber, item.PageSize);
        }

        #endregion
    }
}