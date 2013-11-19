// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The post view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Example.WebUI.Models
{
    /// <summary>
    ///     The post view model.
    /// </summary>
    public class PostViewModel
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     Gets or sets the content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        #endregion
    }
}