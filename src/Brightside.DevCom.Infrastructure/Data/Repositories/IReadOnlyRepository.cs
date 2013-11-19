// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReadOnlyRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The ReadOnlyRepository interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Brightside.DevCom.Infrastructure.Entities;
    using Brightside.DevCom.Infrastructure.Enums;

    /// <summary>
    /// The ReadOnlyRepository interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IReadOnlyRepository<T>
        where T : class, IBusinessObject<T>
    {
        #region Public Properties

        /// <summary>
        ///     Gets the count.
        /// </summary>
        int Count { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The all.
        /// </summary>
        /// <returns>
        ///     The <see cref="IQueryable" />.
        /// </returns>
        IQueryable<T> All();

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Contains(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// The filter.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        IQueryable<T> Filter(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// The filter.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="total">
        /// The total.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        IQueryable<T> Filter(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50);

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="keys">
        /// The keys.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Find(params object[] keys);

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="orderBy">
        /// The order by.
        /// </param>
        /// <param name="includeProperties">
        /// The include properties.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            string includeProperties = "");

        /// <summary>
        /// The get all paged.
        /// </summary>
        /// <param name="take">
        /// The take.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="sortBy">
        /// The sort by.
        /// </param>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="IPagedResult"/>.
        /// </returns>
        IPagedResult<T> GetAllPaged(
            int take, int page = 1, SortBy sortBy = SortBy.Desc, Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T GetById(object id);

        #endregion
    }
}