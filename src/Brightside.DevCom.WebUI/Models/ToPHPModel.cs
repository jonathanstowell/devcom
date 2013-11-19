// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToPHPModel.cs" company="">
//   
// </copyright>
// <summary>
//   The to php model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.WebUI.Models
{
    /// <summary>
    ///     The to php model.
    /// </summary>
    public class ToPHPModel
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether checkbox.
        /// </summary>
        public bool Checkbox { get; set; }

        /// <summary>
        ///     Gets or sets the radio.
        /// </summary>
        public SessionTest Radio { get; set; }

        /// <summary>
        ///     Gets or sets the textbox.
        /// </summary>
        public string Textbox { get; set; }

        #endregion
    }

    /// <summary>
    ///     The session test.
    /// </summary>
    public enum SessionTest
    {
        /// <summary>
        ///     The option 1.
        /// </summary>
        Option1, 

        /// <summary>
        ///     The option 2.
        /// </summary>
        Option2
    }
}