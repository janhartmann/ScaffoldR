namespace ScaffoldR.Core.Commands
{
    /// <summary>
    /// Handles the <typeparamref name="TCommand"/>.
    /// </summary>
    /// <typeparam name="TCommand">The command which should be handled.</typeparam>
    public interface IHandleCommand<TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Handles the <typeparamref name="TCommand"/>.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>An awaitable task.</returns>
        void Handle(TCommand command);
    }
}