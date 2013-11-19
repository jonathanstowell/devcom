// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommand.cs" company="">
//   
// </copyright>
// <summary>
//   The Command interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Web.Conventions
{
    /// <summary>
    ///     The Command interface.
    /// </summary>
    public interface ICommand
    {
        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether has executed.
        /// </summary>
        bool HasExecuted { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The execute.
        /// </summary>
        void Execute();

        #endregion
    }
}