// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommentMap.cs" company="">
//   
// </copyright>
// <summary>
//   The comment map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Data.Maps
{
    using System.Data.Entity.ModelConfiguration;

    using Brightside.DevCom.Entities.Example;

    /// <summary>
    ///     The comment map.
    /// </summary>
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommentMap" /> class.
        /// </summary>
        public CommentMap()
        {
            this.HasKey(t => t.Id);

            this.Property(p => p.Author).IsRequired().HasMaxLength(100);
            this.Property(p => p.Content).IsRequired();

            this.HasRequired(p => p.Post).WithMany(m => m.Comments).Map(m => m.MapKey("PostId"));
        }

        #endregion
    }
}