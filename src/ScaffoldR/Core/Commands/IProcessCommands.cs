namespace ScaffoldR.Core.Commands
{
    /// <summary>
    /// Processes commands.
    /// </summary>
    public interface IProcessCommands
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="command">The command to be executed.</param>
        void Execute(ICommand command);
    }
}
