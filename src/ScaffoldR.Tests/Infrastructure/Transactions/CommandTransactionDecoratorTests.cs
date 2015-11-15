using System.ComponentModel;
using Moq;
using ScaffoldR.Core.Transactions;
using ScaffoldR.Infrastructure.Transactions;
using ScaffoldR.Tests.Infrastructure.Transactions.Fakes;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Transactions
{
    public class CommandTransactionDecoratorTests
    {
        [Fact]
        public void ExecuteTransaction_WhenCommandHasTransactionalAttribute()
        {
            var command = new FakeCommandWithoutValidator();
            Assert.IsAssignableFrom<ICommand>(command);
            
            var transactionProcessor = new Mock<IProcessTransactions>(MockBehavior.Strict);
            transactionProcessor.Setup(x => x.Execute());

            var decorated = new Mock<IHandleCommand<FakeCommandWithoutValidator>>(MockBehavior.Strict);
            TypeDescriptor.AddAttributes(decorated.Object.GetType(), new TransactionalAttribute());
            decorated.Setup(x => x.Handle(command));

            var decorator = new CommandTransactionDecorator<FakeCommandWithoutValidator>(transactionProcessor.Object, () => decorated.Object);
            decorator.Handle(command);

            decorated.Verify(x => x.Handle(command), Times.Once);
            transactionProcessor.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void DoNotExecuteTransaction_WhenCommandDoNotHaveTransactionalAttribute()
        {
            var command = new FakeCommandWithoutValidator();
            Assert.IsAssignableFrom<ICommand>(command);

            var transactionProcessor = new Mock<IProcessTransactions>(MockBehavior.Strict);
            transactionProcessor.Setup(x => x.Execute());

            var decorated = new Mock<IHandleCommand<FakeCommandWithoutValidator>>(MockBehavior.Strict);
            decorated.Setup(x => x.Handle(command));

            var decorator = new CommandTransactionDecorator<FakeCommandWithoutValidator>(transactionProcessor.Object, () => decorated.Object);
            decorator.Handle(command);

            decorated.Verify(x => x.Handle(command), Times.Once);
            transactionProcessor.Verify(x => x.Execute(), Times.Never);
        }

    }
}
