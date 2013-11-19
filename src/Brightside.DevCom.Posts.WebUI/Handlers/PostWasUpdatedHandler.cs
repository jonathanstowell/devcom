// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostWasUpdatedHandler.cs" company="">
//   
// </copyright>
// <summary>
//   The post was updated handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Posts.WebUI.Handlers
{
    using System;

    using Brightside.DevCom.Events.Posts;
    using Brightside.DevCom.Posts.WebUI.Hubs;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Infrastructure;

    using ReallySimpleEventing.EventHandling;

    /// <summary>
    ///     The post was updated handler.
    /// </summary>
    public class PostWasUpdatedHandler : IHandle<PostWasUpdated>
    {
        #region Fields

        /// <summary>
        ///     The connection manager.
        /// </summary>
        private readonly IConnectionManager connectionManager;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostWasUpdatedHandler"/> class.
        /// </summary>
        /// <param name="connectionManager">
        /// The connection manager.
        /// </param>
        public PostWasUpdatedHandler(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="event">
        /// The event.
        /// </param>
        public void Handle(PostWasUpdated @event)
        {
            IHubContext hub = this.connectionManager.GetHubContext<PostHub>();
            hub.Clients.All.updatedPost(@event.Post);
        }

        /// <summary>
        /// The on error.
        /// </summary>
        /// <param name="event">
        /// The event.
        /// </param>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public void OnError(PostWasUpdated @event, Exception ex)
        {
        }

        #endregion
    }
}