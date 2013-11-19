// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewKeyGenerator.cs" company="">
//   
// </copyright>
// <summary>
//   The view key generator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.Plugin.Views
{
    /// <summary>
    ///     The view key generator.
    /// </summary>
    public static class ViewKeyGenerator
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get view key.
        /// </summary>
        /// <param name="componentname">
        /// The componentname.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetViewKey(string componentname)
        {
            string[] keySplit = componentname.Split('.');
            int lastIndex = keySplit.Length;
            string result = string.Format("{0}.{1}", keySplit[lastIndex - 2], keySplit[lastIndex - 1]);

            return result;
        }

        #endregion
    }
}