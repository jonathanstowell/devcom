// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="">
//   
// </copyright>
// <summary>
//   The extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Eventing
{
    using System;

    using ReallySimpleEventing;

    /// <summary>
    ///     The extensions.
    /// </summary>
    public static class Extensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The raise.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <param name="eventConstructor">
        /// The event constructor.
        /// </param>
        /// <typeparam name="TEvent">
        /// </typeparam>
        public static void Raise<TEvent>(this IEventStream stream, Action<TEvent> eventConstructor) where TEvent : new()
        {
            var @event = new TEvent();
            eventConstructor(@event);
            stream.Raise(@event);
        }

        #endregion
    }
}