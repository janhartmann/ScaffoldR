using System.Diagnostics;
using ScaffoldR.Core.Transactions;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.Transactions
{
    internal sealed class CommandProcessor : IProcessCommands
    {
        private readonly Container _container;

        public CommandProcessor(Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public void Execute(ICommand command)
        {
            var handlerType = typeof(IHandleCommand<>).MakeGenericType(command.GetType());
            dynamic handler = _container.GetInstance(handlerType);
            
            handler.Handle((dynamic)command);
        }
    }
}