using System.ComponentModel;
using Moq;
using ScaffoldR.Core.Events;
using ScaffoldR.Core.Transactions;
using ScaffoldR.Infrastructure.Transactions;
using ScaffoldR.Tests.Infrastructure.Transactions.Fakes;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Transactions
{
    public class CommandEventProcessingDecoratorTests
    {
        [Fact]
        public void RaiseEvent_AfterCommandIsExecuted_WhenCommandHasRaiseEventAttributeAndEnabledTrue()
        {
            var command = new FakeCommandWithoutValidator();
            Assert.IsAssignableFrom<ICommand>(command);
            
            var eventProcessor = new Mock<IProcessEvents>(MockBehavior.Strict);
            eventProcessor.Setup(x => x.Raise(command));

            var attribute = new RaiseEventAttribute
            {
                Enabled = true
            };

            var decorated = new Mock<IHandleCommand<FakeCommandWithoutValidator>>(MockBehavior.Strict);
            TypeDescriptor.AddAttributes(decorated.Object.GetType(), attribute);
            decorated.Setup(x => x.Handle(command));

            var decorator = new CommandEventProcessingDecorator<FakeCommandWithoutValidator>(eventProcessor.Object, () => decorated.Object);
            decorator.Handle(command);

            decorated.Verify(x => x.Handle(command), Times.Once);
            eventProcessor.Verify(x => x.Raise(command), Times.Once);
        }

        [Fact]
        public void DoNotRaiseEvent_AfterCommandIsExecuted_WhenCommandHasRaiseEventAttributeAndEnabledFalse()
        {
            var command = new FakeCommandWithoutValidator();
            Assert.IsAssignableFrom<ICommand>(command);

            var eventProcessor = new Mock<IProcessEvents>(MockBehavior.Strict);
            eventProcessor.Setup(x => x.Raise(command));

            var attribute = new RaiseEventAttribute
            {
                Enabled = false
            };

            var decorated = new Mock<IHandleCommand<FakeCommandWithoutValidator>>(MockBehavior.Strict);
            TypeDescriptor.AddAttributes(decorated.Object.GetType(), attribute);
            decorated.Setup(x => x.Handle(command));

            var decorator = new CommandEventProcessingDecorator<FakeCommandWithoutValidator>(eventProcessor.Object, () => decorated.Object);
            decorator.Handle(command);

            decorated.Verify(x => x.Handle(command), Times.Once);
            eventProcessor.Verify(x => x.Raise(It.IsAny<IEvent>()), Times.Never);
        }

        [Fact]
        public void DoNotRaiseEvent_AfterCommandIsExecuted_WhenCommandDoesNotHaveRaiseEventAttribute()
        {
            var command = new FakeCommandWithoutValidator();
            Assert.IsAssignableFrom<ICommand>(command);

            var eventProcessor = new Mock<IProcessEvents>(MockBehavior.Strict);
            eventProcessor.Setup(x => x.Raise(command));

            var decorated = new Mock<IHandleCommand<FakeCommandWithoutValidator>>(MockBehavior.Strict);
            decorated.Setup(x => x.Handle(command));

            var decorator = new CommandEventProcessingDecorator<FakeCommandWithoutValidator>(eventProcessor.Object, () => decorated.Object);
            decorator.Handle(command);

            decorated.Verify(x => x.Handle(command), Times.Once);
            eventProcessor.Verify(x => x.Raise(It.IsAny<IEvent>()), Times.Never);
        }

    }
}
