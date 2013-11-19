// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostsController.cs" company="">
//   
// </copyright>
// <summary>
//   The posts controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Posts.WebUI.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Brightside.DevCom.Entities.Posts;
    using Brightside.DevCom.Infrastructure.Web.Extensions;
    using Brightside.DevCom.Posts.WebUI.Commands;
    using Brightside.DevCom.Posts.WebUI.Models;
    using Brightside.DevCom.Service;

    using Microsoft.AspNet.SignalR.Infrastructure;

    /// <summary>
    ///     The posts controller.
    /// </summary>
    public class PostsController : ApiController
    {
        #region Fields

        /// <summary>
        ///     The connection manager.
        /// </summary>
        private readonly IConnectionManager connectionManager;

        /// <summary>
        ///     The create post command accessor.
        /// </summary>
        private readonly Func<CreatePostCommand> createPostCommandAccessor;

        /// <summary>
        ///     The service.
        /// </summary>
        private readonly IPostService service;

        /// <summary>
        ///     The update post command accessor.
        /// </summary>
        private readonly Func<UpdatePostCommand> updatePostCommandAccessor;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostsController"/> class.
        /// </summary>
        /// <param name="service">
        /// The service.
        /// </param>
        /// <param name="createPostCommandAccessor">
        /// The create post command accessor.
        /// </param>
        /// <param name="updatePostCommandAccessor">
        /// The update post command accessor.
        /// </param>
        /// <param name="connectionManager">
        /// The connection manager.
        /// </param>
        public PostsController(
            IPostService service, 
            Func<CreatePostCommand> createPostCommandAccessor, 
            Func<UpdatePostCommand> updatePostCommandAccessor, 
            IConnectionManager connectionManager)
        {
            this.service = service;

            this.createPostCommandAccessor = createPostCommandAccessor;
            this.updatePostCommandAccessor = updatePostCommandAccessor;

            this.connectionManager = connectionManager;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get.
        /// </summary>
        /// <returns>
        ///     The <see cref="IList" />.
        /// </returns>
        /// <exception cref="HttpResponseException">
        /// </exception>
        public IList<Post> Get()
        {
            IList<Post> posts = this.service.Get();

            if (posts == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return posts;
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Post"/>.
        /// </returns>
        /// <exception cref="HttpResponseException">
        /// </exception>
        public Post Get(int id)
        {
            Post post = this.service.GetById(id);

            if (post == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return post;
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        public HttpResponseMessage Post(PostViewModel model)
        {
            CreatePostCommand cmd = this.createPostCommandAccessor();

            cmd.Author = model.Author;
            cmd.Content = model.Content;

            cmd.Execute();

            if (!cmd.Results.IsValid)
            {
                return this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, new HttpError("Validation Error") { { "Results", cmd.Results.ToDTO() } });
            }

            if (!cmd.IsSuccessful)
            {
                return this.Request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError, new HttpError("Internal Server Error"));
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, cmd.Post);
        }

        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        public HttpResponseMessage Put(PostViewModel model)
        {
            UpdatePostCommand cmd = this.updatePostCommandAccessor();

            cmd.Id = model.Id;
            cmd.Author = model.Author;
            cmd.Content = model.Content;

            cmd.Execute();

            if (!cmd.Results.IsValid)
            {
                return this.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, new HttpError("Validation Error") { { "Results", cmd.Results.ToDTO() } });
            }

            if (!cmd.IsSuccessful)
            {
                return this.Request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError, new HttpError("Internal Server Error"));
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, cmd.Post);
        }

        #endregion
    }
}