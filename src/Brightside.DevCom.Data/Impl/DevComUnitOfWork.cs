// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DevComUnitOfWork.cs" company="">
//   
// </copyright>
// <summary>
//   The brightside unit of work.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Data.Impl
{
    using System;

    using Brightside.DevCom.Infrastructure.Data;

    /// <summary>
    ///     The brightside unit of work.
    /// </summary>
    public class DevComUnitOfWork : IDevComUnitOfWork
    {
        #region Fields

        /// <summary>
        ///     The context.
        /// </summary>
        private readonly DevComContext context;

        /// <summary>
        /// The post repository.
        /// </summary>
        private readonly IPostRepository postRepository;

        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DevComUnitOfWork"/> class.
        /// </summary>
        /// <param name="contextFactory">
        /// The context factory.
        /// </param>
        /// <param name="postRepository">
        /// The post Repository.
        /// </param>
        public DevComUnitOfWork(IDatabaseContextFactory<DevComContext> contextFactory, IPostRepository postRepository)
        {
            this.postRepository = postRepository;
            this.context = contextFactory.GetContext();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the post repository.
        /// </summary>
        public IPostRepository PostRepository
        {
            get
            {
                return this.postRepository;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     The save.
        /// </summary>
        public void Save()
        {
            this.context.SaveChanges();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }

        #endregion
    }
}