using System.Threading;
using ScaffoldR.Core.Events;
using ScaffoldR.Tests.Infrastructure.CompositionRoot;
using ScaffoldR.Tests.Infrastructure.Events.Fakes;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Events
{
    [Collection("Simple Injector Tests")]
    public class EventProcessorTests
    {
        private readonly CompositionRootFixture _fixture;

        public EventProcessorTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Execute_InvokesEventHandler_UsingContainerForResolution()
        {
            var events = _fixture.Container.GetInstance<IProcessEvents>();
            var @event = new FakeEventWithoutValidation();
            events.Raise(@event);
            Thread.Sleep(1000); // :-(
            Assert.Equal("faked", @event.ReturnValue);
        }
    }
}
