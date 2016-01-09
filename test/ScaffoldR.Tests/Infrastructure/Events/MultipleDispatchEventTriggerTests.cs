using System.Collections.Generic;
using Moq;
using ScaffoldR.Core.Events;
using ScaffoldR.Infrastructure.Events;
using ScaffoldR.Tests.Infrastructure.Events.Fakes;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Events
{
    public class MultipleDispatchEventTriggerTests
    {
        [Fact]
        public void HandleAllEventHandlers_WhenMultipleEventHandlersExists()
        {
            var evt = new FakeEventWithoutValidation();
            Assert.IsAssignableFrom<IEvent>(evt);

            var handlerOneCallback = string.Empty;
            var handlerOne = new Mock<IHandleEvent<FakeEventWithoutValidation>>(MockBehavior.Strict);
            handlerOne.Setup(x => x.Handle(evt)).Callback(() => handlerOneCallback = "handlerOneCallback");

            var handlerTwoCallback = string.Empty;
            var handlerTwo = new Mock<IHandleEvent<FakeEventWithoutValidation>>(MockBehavior.Strict);
            handlerTwo.Setup(x => x.Handle(evt)).Callback(() => handlerTwoCallback = "handlerTwoCallback");

            var handlers = new List<IHandleEvent<FakeEventWithoutValidation>>
            {
                handlerOne.Object,
                handlerTwo.Object
            };
            
            var decorator = new MultipleDispatchEventTrigger<FakeEventWithoutValidation>(handlers);
            decorator.Trigger(evt);

            handlerOne.Verify(x => x.Handle(evt), Times.Once);
            handlerTwo.Verify(x => x.Handle(evt), Times.Once);

            Assert.Equal("handlerOneCallback", handlerOneCallback);
            Assert.Equal("handlerTwoCallback", handlerTwoCallback);
        }

    }
}
