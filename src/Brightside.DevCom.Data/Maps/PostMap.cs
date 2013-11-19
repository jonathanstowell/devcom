// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostMap.cs" company="">
//   
// </copyright>
// <summary>
//   The post map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Data.Maps
{
    using System.Data.Entity.ModelConfiguration;

    using Brightside.DevCom.Entities.Example;

    /// <summary>
    ///     The post map.
    /// </summary>
    public class PostMap : EntityTypeConfiguration<Post>
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PostMap" /> class.
        /// </summary>
        public PostMap()
        {
            this.HasKey(t => t.Id);

            this.Property(p => p.Author).IsRequired().HasMaxLength(100);
            this.Property(p => p.Content).IsRequired();
        }

        #endregion
    }
}