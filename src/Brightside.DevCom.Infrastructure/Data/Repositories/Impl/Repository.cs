// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="">
//   
// </copyright>
// <summary>
//   The repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Data.Repositories.Impl
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;

    using Brightside.DevCom.Infrastructure.Entities;

    /// <summary>
    /// The repository.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="T1">
    /// </typeparam>
    public class Repository<T, T1> : ReadOnlyRepository<T, T1>, IRepository<T>
        where T : class, IBusinessObject<T> where T1 : DbContext
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T,T1}"/> class. 
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="contextFactory">
        /// The context Factory.
        /// </param>
        public Repository(IDatabaseContextFactory<T1> contextFactory)
            : base(contextFactory)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T Create(T entity)
        {
            T newEntry = this.set.Add(entity);
            return newEntry;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public virtual void Delete(object id)
        {
            T entityToDelete = this.set.Find(id);
            this.Delete(entityToDelete);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public virtual void Delete(T entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            this.set.Remove(entity);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> entitiesToDelete = this.Filter(predicate);
            foreach (T entity in entitiesToDelete)
            {
                if (this.context.Entry(entity).State == EntityState.Detached)
                {
                    this.set.Attach(entity);
                }

                this.set.Remove(entity);
            }
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public virtual void Update(T entity)
        {
            DbEntityEntry<T> entry = this.context.Entry(entity);
            this.set.Attach(entity);
            entry.State = EntityState.Modified;
        }

        #endregion
    }
}