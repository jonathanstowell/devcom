// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusinessObject.cs" company="">
//   
// </copyright>
// <summary>
//   The business object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Entities.Impl
{
    using Newtonsoft.Json;

    /// <summary>
    /// The business object.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    /// <typeparam name="TId">
    /// </typeparam>
    public abstract class BusinessObject<TEntity, TId>
        where TEntity : BusinessObject<TEntity, TId>, IBusinessObject<TEntity, TId>
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public virtual TId Id { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var other = obj as TEntity;

            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (!Equals(default(TId), this.Id) && !Equals(default(TId), other.Id))
            {
                return Equals(this.Id, other.Id);
            }

            return this.Equals(other);
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Equals(TEntity other)
        {
            return false;
        }

        /// <summary>
        ///     The get hash code.
        /// </summary>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public override int GetHashCode()
        {
            return this.Id == null ? 0 : this.Id.GetHashCode();
        }

        #endregion
    }

    /// <summary>
    /// The business object.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public abstract class BusinessObject<TEntity> : BusinessObject<TEntity, int>
        where TEntity : BusinessObject<TEntity>, IBusinessObject<TEntity>
    {
    }
}