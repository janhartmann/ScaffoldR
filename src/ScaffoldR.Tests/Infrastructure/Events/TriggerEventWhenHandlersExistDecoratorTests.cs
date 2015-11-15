using System;
using System.Collections.Generic;
using Moq;
using ScaffoldR.Core.Events;
using ScaffoldR.Core.Tasks;
using ScaffoldR.Infrastructure.Events;
using ScaffoldR.Tests.Infrastructure.Events.Fakes;
using SimpleInjector;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Events
{
    public class TriggerEventWhenHandlersExistDecoratorTests
    {
        [Fact]
        public void TriggerEventHandler_WhenEventHandlersExist()
        {
            var container = new Container();

            var evt = new FakeEventWithoutValidation();
            Assert.IsAssignableFrom<IEvent>(evt);

            var handlers = new List<IHandleEvent<FakeEventWithoutValidation>> {new HandleFakeEventWithoutValidation()};

            var decorated = new Mock<ITriggerEvent<FakeEventWithoutValidation>>(MockBehavior.Strict);
            decorated.Setup(x => x.Trigger(evt));

            var taskFactory = new Mock<ITaskFactory>(MockBehavior.Strict);
            taskFactory.Setup(factory => factory.StartTask(It.IsAny<Action>())).Callback<Action>(action => action());         

            var decorator = new TriggerEventWhenHandlersExistDecorator<FakeEventWithoutValidation>(container, taskFactory.Object, () => decorated.Object, handlers);
            decorator.Trigger(evt);

            taskFactory.Verify(x => x.StartTask(It.IsAny<Action>()), Times.Once);
            decorated.Verify(x => x.Trigger(evt), Times.Once);
        }

        [Fact]
        public void DoNotTriggerEventHandler_WhenEventHandlersDoesNotExist()
        {
            var container = new Container();

            var evt = new FakeEventWithoutValidation();
            Assert.IsAssignableFrom<IEvent>(evt);

            var decorated = new Mock<ITriggerEvent<FakeEventWithoutValidation>>(MockBehavior.Strict);
            decorated.Setup(x => x.Trigger(evt));

            var taskFactory = new Mock<ITaskFactory>(MockBehavior.Strict);
            taskFactory.Setup(factory => factory.StartTask(It.IsAny<Action>())).Callback<Action>(action => action());

            var decorator = new TriggerEventWhenHandlersExistDecorator<FakeEventWithoutValidation>(container, taskFactory.Object, () => decorated.Object, null);
            decorator.Trigger(evt);

            taskFactory.Verify(x => x.StartTask(It.IsAny<Action>()), Times.Never);
            decorated.Verify(x => x.Trigger(evt), Times.Never);
        }
    }
}
