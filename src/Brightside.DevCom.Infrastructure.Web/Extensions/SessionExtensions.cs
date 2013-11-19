// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The session extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.Extensions
{
    using System.Web.SessionState;

    /// <summary>
    ///     The session extensions.
    /// </summary>
    public static class SessionExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get value.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T GetValue<T>(this HttpSessionState session, string key) where T : struct
        {
            if (session == null)
            {
                return default(T);
            }

            if (session[key] == null)
            {
                return default(T);
            }

            return (T)session[key];
        }

        /// <summary>
        /// The set.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void Set<T>(this HttpSessionState session, string key, T value)
        {
            if (session == null)
            {
                return;
            }

            session[key] = value;
        }

        /// <summary>
        /// The try get.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T TryGet<T>(this HttpSessionState session, string key) where T : class
        {
            if (session == null)
            {
                return null;
            }

            if (session[key] == null)
            {
                return null;
            }

            return (T)session[key];
        }

        /// <summary>
        /// The try get value.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T?"/>.
        /// </returns>
        public static T? TryGetValue<T>(this HttpSessionState session, string key) where T : struct
        {
            if (session == null)
            {
                return null;
            }

            if (session[key] == null)
            {
                return null;
            }

            return (T?)session[key];
        }

        #endregion
    }
}