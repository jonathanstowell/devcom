// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DevComContext.cs" company="">
//   
// </copyright>
// <summary>
//   The brightside context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Data.Impl
{
    using System.Data.Entity;

    using Brightside.DevCom.Data.Maps;
    using Brightside.DevCom.Entities.Posts;

    /// <summary>
    ///     The brightside context.
    /// </summary>
    public class DevComContext : DbContext
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public DbSet<Comment> Comment { get; set; }

        /// <summary>
        ///     Gets or sets the posts.
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new PostMap());
            modelBuilder.Configurations.Add(new CommentMap());
        }

        #endregion
    }
}