using System.ComponentModel;
using Moq;
using ScaffoldR.Core.Commands;
using ScaffoldR.Core.Events;
using ScaffoldR.Infrastructure.Commands;
using ScaffoldR.Tests.Infrastructure.Commands.Fakes;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Commands
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
            var provider = TypeDescriptor.AddAttributes(decorated.Object.GetType(), attribute);
            decorated.Setup(x => x.Handle(command));

            var decorator = new CommandEventProcessingDecorator<FakeCommandWithoutValidator>(eventProcessor.Object, () => decorated.Object);
            decorator.Handle(command);

            decorated.Verify(x => x.Handle(command), Times.Once);
            eventProcessor.Verify(x => x.Raise(command), Times.Once);

            // Clean up for next test
            TypeDescriptor.RemoveProvider(provider, decorated.Object.GetType());
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
            var provider = TypeDescriptor.AddAttributes(decorated.Object.GetType(), attribute);
            decorated.Setup(x => x.Handle(command));

            var decorator = new CommandEventProcessingDecorator<FakeCommandWithoutValidator>(eventProcessor.Object, () => decorated.Object);
            decorator.Handle(command);

            decorated.Verify(x => x.Handle(command), Times.Once);
            eventProcessor.Verify(x => x.Raise(It.IsAny<IEvent>()), Times.Never);

            // Clean up for next test
            TypeDescriptor.RemoveProvider(provider, decorated.Object.GetType());
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
