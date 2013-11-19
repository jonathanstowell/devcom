// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HybridPerWebRequestScopedAccessor.cs" company="">
//   
// </copyright>
// <summary>
//   The hybrid per web request scoped accessor.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Castle
{
    using global::Castle.MicroKernel.Lifestyle;

    /// <summary>
    ///     The hybrid per web request scoped accessor.
    /// </summary>
    public class HybridPerWebRequestScopedAccessor : HybridPerWebRequestScopeAccessor
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HybridPerWebRequestScopedAccessor" /> class.
        /// </summary>
        public HybridPerWebRequestScopedAccessor()
            : base(new LifetimeScopeAccessor())
        {
        }

        #endregion
    }
}