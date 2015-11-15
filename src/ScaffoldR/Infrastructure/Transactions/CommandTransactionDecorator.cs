using System;
using ScaffoldR.Core.Extensions;
using ScaffoldR.Core.Transactions;

namespace ScaffoldR.Infrastructure.Transactions
{
    internal sealed class CommandTransactionDecorator<TCommand> : IHandleCommand<TCommand> where TCommand : ICommand
    {
        private readonly IProcessTransactions _transactions;
        private readonly Func<IHandleCommand<TCommand>> _handlerFactory;

        public CommandTransactionDecorator(IProcessTransactions transactions, Func<IHandleCommand<TCommand>> handlerFactory)
        {
            _transactions = transactions;
            _handlerFactory = handlerFactory;
        }

        public void Handle(TCommand command)
        {
            var handler = _handlerFactory();
            handler.Handle(command);

            var attribute = handler.GetType().GetRuntimeAddedAttribute<TransactionalAttribute>();
            if (attribute != null)
            {
                _transactions.Execute();
            }
        }
    }
}