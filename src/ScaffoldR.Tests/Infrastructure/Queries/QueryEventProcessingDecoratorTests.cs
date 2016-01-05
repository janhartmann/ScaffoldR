using System.ComponentModel;
using Moq;
using ScaffoldR.Core.Events;
using ScaffoldR.Core.Queries;
using ScaffoldR.Infrastructure.Queries;
using ScaffoldR.Tests.Infrastructure.Queries.Fakes;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Queries
{
    public class QueryEventProcessingDecoratorTests
    {
        [Fact]
        public void RaiseEvent_AfterQueryIsExecuted_WhenQueryHasRaiseEventAttributeAndEnabledTrue()
        {
            var query = new FakeQueryWithoutValidator();
            Assert.IsAssignableFrom<IQuery<string>>(query);
            
            var eventProcessor = new Mock<IProcessEvents>(MockBehavior.Strict);
            eventProcessor.Setup(x => x.Raise(query));

            var attribute = new RaiseEventAttribute
            {
                Enabled = true
            };

            var decorated = new Mock<IHandleQuery<FakeQueryWithoutValidator, string>>(MockBehavior.Strict);
            var provider = TypeDescriptor.AddAttributes(decorated.Object.GetType(), attribute);
            decorated.Setup(x => x.Handle(query)).Returns("faked");

            var decorator = new QueryEventProcessingDecorator<FakeQueryWithoutValidator, string>(eventProcessor.Object, () => decorated.Object);
            decorator.Handle(query);

            decorated.Verify(x => x.Handle(query), Times.Once);
            eventProcessor.Verify(x => x.Raise(query), Times.Once);

            // Clean up runtime added attributes
            TypeDescriptor.RemoveProvider(provider, decorated.Object.GetType());
        }

        [Fact]
        public void DoNotRaiseEvent_AfterQueryIsExecuted_WhenQueryHasRaiseEventAttributeAndEnabledFalse()
        {
            var query = new FakeQueryWithoutValidator();
            Assert.IsAssignableFrom<IQuery<string>>(query);

            var eventProcessor = new Mock<IProcessEvents>(MockBehavior.Strict);
            eventProcessor.Setup(x => x.Raise(query));

            var attribute = new RaiseEventAttribute
            {
                Enabled = false
            };

            var decorated = new Mock<IHandleQuery<FakeQueryWithoutValidator, string>>(MockBehavior.Strict);
            var provider = TypeDescriptor.AddAttributes(decorated.Object.GetType(), attribute);
            decorated.Setup(x => x.Handle(query)).Returns("faked");

            var decorator = new QueryEventProcessingDecorator<FakeQueryWithoutValidator, string>(eventProcessor.Object, () => decorated.Object);
            decorator.Handle(query);

            decorated.Verify(x => x.Handle(query), Times.Once);
            eventProcessor.Verify(x => x.Raise(It.IsAny<IEvent>()), Times.Never);

            // Clean up runtime added attributes
            TypeDescriptor.RemoveProvider(provider, decorated.Object.GetType());
        }

        [Fact]
        public void DoNotRaiseEvent_AfterQueryIsExecuted_WhenQueryDoesNotHaveRaiseEventAttribute()
        {
            var query = new FakeQueryWithoutValidator();
            Assert.IsAssignableFrom<IQuery<string>>(query);

            var eventProcessor = new Mock<IProcessEvents>(MockBehavior.Strict);
            eventProcessor.Setup(x => x.Raise(query));

            var decorated = new Mock<IHandleQuery<FakeQueryWithoutValidator, string>>(MockBehavior.Strict);
            decorated.Setup(x => x.Handle(query)).Returns("faked");

            var decorator = new QueryEventProcessingDecorator<FakeQueryWithoutValidator, string>(eventProcessor.Object, () => decorated.Object);
            decorator.Handle(query);

            decorated.Verify(x => x.Handle(query), Times.Once);
            eventProcessor.Verify(x => x.Raise(It.IsAny<IEvent>()), Times.Never);
        }

    }
}
