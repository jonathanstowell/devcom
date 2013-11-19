// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DevComDatabaseContextFactory.cs" company="">
//   
// </copyright>
// <summary>
//   The brightside database context factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brightside.DevCom.Data.Impl
{
    using Brightside.DevCom.Infrastructure.Data;

    /// <summary>
    ///     The brightside database context factory.
    /// </summary>
    public class DevComDatabaseContextFactory : IDatabaseContextFactory<DevComContext>
    {
        #region Fields

        /// <summary>
        ///     The context.
        /// </summary>
        private readonly DevComContext context;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DevComDatabaseContextFactory" /> class.
        /// </summary>
        public DevComDatabaseContextFactory()
        {
            this.context = new DevComContext();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get context.
        /// </summary>
        /// <returns>
        ///     The <see cref="DevComContext" />.
        /// </returns>
        public DevComContext GetContext()
        {
            return this.context;
        }

        #endregion
    }
}