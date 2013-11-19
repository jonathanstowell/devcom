// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindsorActivationStrategy.cs" company="">
//   
// </copyright>
// <summary>
//   The windsor activation strategy.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Brightside.DevCom.Infrastructure.Eventing
{
    using System.Collections.Generic;

    using global::Castle.Windsor;

    using ReallySimpleEventing.ActivationStrategies;
    using ReallySimpleEventing.EventHandling;

    /// <summary>
    ///     The windsor activation strategy.
    /// </summary>
    public class WindsorActivationStrategy : IHandlerActivationStrategy
    {
        #region Fields

        /// <summary>
        ///     The container.
        /// </summary>
        private readonly IWindsorContainer container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorActivationStrategy"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public WindsorActivationStrategy(IWindsorContainer container)
        {
            this.container = container;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get handlers.
        /// </summary>
        /// <typeparam name="TEventType">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public IEnumerable<IHandle<TEventType>> GetHandlers<TEventType>()
        {
            return this.container.ResolveAll<IHandle<TEventType>>();
        }

        /// <summary>
        /// The on handler executed.
        /// </summary>
        /// <param name="handler">
        /// The handler.
        /// </param>
        /// <typeparam name="TEventType">
        /// </typeparam>
        public void OnHandlerExecuted<TEventType>(IHandle<TEventType> handler)
        {
        }

        #endregion
    }
}