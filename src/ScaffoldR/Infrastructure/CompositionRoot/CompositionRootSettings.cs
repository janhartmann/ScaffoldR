using System.Reflection;
using FluentValidation;
using ScaffoldR.Core.Events;
using ScaffoldR.Core.Transactions;
using ScaffoldR.Core.Views;

namespace ScaffoldR.Infrastructure.CompositionRoot
{
    /// <summary>
    /// The main composition settings.
    /// </summary>
    public class CompositionRootSettings
    {
        /// <summary>
        /// Where the <see cref="IHandleCommand{TCommand}"/> and <see cref="IHandleQuery{TQuery,TResult}"/> are located.
        /// </summary>
        public Assembly[] TransactionAssemblies { get; set; }

        /// <summary>
        /// Where the <see cref="IHandleEvent{TEvent}"/> are located.
        /// </summary>
        public Assembly[] EventAssemblies { get; set; }

        /// <summary>
        /// Where the <see cref="AbstractValidator{T}"/> are located
        /// </summary>
        public Assembly[] FluentValidationAssemblies { get; set; }

        /// <summary>
        /// Where the <see cref="IHandleViewModel{TViewModel}"/> are located.
        /// </summary>
        public Assembly[] ViewModelAssemblies { get; set; }
    }
}
