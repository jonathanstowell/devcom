// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadOnlyRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The read only repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Data.Repositories.Impl
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using Brightside.DevCom.Infrastructure.Entities;
    using Brightside.DevCom.Infrastructure.Entities.Impl;
    using Brightside.DevCom.Infrastructure.Enums;

    /// <summary>
    /// The read only repository.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="T1">
    /// </typeparam>
    public class ReadOnlyRepository<T, T1> : IReadOnlyRepository<T>
        where T : class, IBusinessObject<T> where T1 : DbContext
    {
        #region Fields

        /// <summary>
        ///     The context.
        /// </summary>
        protected readonly DbContext context;

        /// <summary>
        ///     The set.
        /// </summary>
        protected readonly DbSet<T> set;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyRepository{T,T1}"/> class. 
        /// Initializes a new instance of the <see cref="ReadOnlyRepository{T}"/> class.
        /// </summary>
        /// <param name="contextFactory">
        /// The context Factory.
        /// </param>
        public ReadOnlyRepository(IDatabaseContextFactory<T1> contextFactory)
        {
            this.context = contextFactory.GetContext();
            this.set = this.context.Set<T>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the count.
        /// </summary>
        public virtual int Count
        {
            get
            {
                return this.set.Count();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The all.
        /// </summary>
        /// <returns>
        ///     The <see cref="IQueryable" />.
        /// </returns>
        public virtual IQueryable<T> All()
        {
            return this.set.AsQueryable();
        }

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return this.set.Count(predicate) > 0;
        }

        /// <summary>
        /// The filter.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return this.set.Where(predicate).AsQueryable();
        }

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
        public virtual IQueryable<T> Filter(
            Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            IQueryable<T> resetSet = filter != null ? this.set.Where(filter).AsQueryable() : this.set.AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="keys">
        /// The keys.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T Find(params object[] keys)
        {
            return this.set.Find(keys);
        }

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return this.set.FirstOrDefault(predicate);
        }

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
        public virtual IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            string includeProperties = "")
        {
            IQueryable<T> query = this.set;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (string includeProperty in
                    includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).AsQueryable();
            }

            return query.AsQueryable();
        }

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
        public virtual IPagedResult<T> GetAllPaged(
            int take, int page = 1, SortBy sortBy = SortBy.Desc, Expression<Func<T, bool>> filter = null)
        {
            int firstResult = (page - 1) * take;

            IQueryable<T> query = this.Include(this.set);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            switch (sortBy)
            {
                case SortBy.Asc:
                    query = query.OrderBy(x => x.Id);
                    break;
                case SortBy.Desc:
                    query = query.OrderByDescending(x => x.Id);
                    break;
            }

            query = query.Skip(firstResult).Take(take);

            int recordsInThisQuery = filter != null ? this.set.Count(filter) : this.set.Count();

            return new PagedResult<T>(query.ToList(), recordsInThisQuery, page, take);
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T GetById(object id)
        {
            return this.set.Find(id);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The include.
        /// </summary>
        /// <param name="set">
        /// The set.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        protected virtual IQueryable<T> Include(DbSet<T> set)
        {
            return set;
        }

        #endregion
    }
}