// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PagedResult.cs" company="">
//   
// </copyright>
// <summary>
//   The paged result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Infrastructure.Entities.Impl
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The paged result.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class PagedResult<T> : IPagedResult<T>
        where T : class
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResult{T}"/> class.
        /// </summary>
        public PagedResult()
        {
            this.Items = new List<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResult{T}"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <param name="totalItemCount">
        /// The total item count.
        /// </param>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        public PagedResult(IEnumerable<T> items, int totalItemCount, int pageNumber, int pageSize)
            : this()
        {
            this.Items = items;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalItemCount = totalItemCount;

            this.Initialize(items);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether has next page.
        /// </summary>
        [JsonProperty(PropertyName = "hasNextPage")]
        public bool HasNextPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has previous page.
        /// </summary>
        [JsonProperty(PropertyName = "hasPreviousPage")]
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is first page.
        /// </summary>
        [JsonProperty(PropertyName = "isFirstPage")]
        public bool IsFirstPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is last page.
        /// </summary>
        [JsonProperty(PropertyName = "isLastPage")]
        public bool IsLastPage { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Gets or sets the page count.
        /// </summary>
        [JsonProperty(PropertyName = "pageCount")]
        public int PageCount { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        [JsonProperty(PropertyName = "pageNumber")]
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        [JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total item count.
        /// </summary>
        [JsonProperty(PropertyName = "totalItemCount")]
        public int TotalItemCount { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The initialize.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        protected void Initialize(IEnumerable<T> items)
        {
            if (this.TotalItemCount == 0)
            {
                this.PageCount = 1;
            }
            else
            {
                this.PageCount = (int)Math.Ceiling(this.TotalItemCount / (double)this.PageSize);
            }

            this.HasPreviousPage = this.PageNumber > 1;
            this.HasNextPage = this.PageNumber < this.PageCount;
            this.IsFirstPage = this.PageNumber == 1;
            this.IsLastPage = this.PageNumber == this.PageCount;
        }

        #endregion
    }
}