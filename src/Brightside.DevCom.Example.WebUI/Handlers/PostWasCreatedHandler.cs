// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostWasCreatedHandler.cs" company="">
//   
// </copyright>
// <summary>
//   The post was created handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Example.WebUI.Handlers
{
    using System;

    using Brightside.DevCom.Events.Post;
    using Brightside.DevCom.Example.WebUI.Hubs;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Infrastructure;

    using ReallySimpleEventing.EventHandling;

    /// <summary>
    ///     The post was created handler.
    /// </summary>
    public class PostWasCreatedHandler : IHandle<PostWasCreated>
    {
        #region Fields

        /// <summary>
        ///     The connection manager.
        /// </summary>
        private readonly IConnectionManager connectionManager;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostWasCreatedHandler"/> class.
        /// </summary>
        /// <param name="connectionManager">
        /// The connection manager.
        /// </param>
        public PostWasCreatedHandler(IConnectionManager connectionManager)
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
        public void Handle(PostWasCreated @event)
        {
            IHubContext hub = this.connectionManager.GetHubContext<PostHub>();
            hub.Clients.All.createdPost(@event.Post);
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
        public void OnError(PostWasCreated @event, Exception ex)
        {
        }

        #endregion
    }
}