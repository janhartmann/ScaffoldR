using ScaffoldR.Core.Events;

namespace ScaffoldR.Tests.Infrastructure.Events.Fakes
{
    public class FakeEventWithoutValidation : IEvent
    {
        public string ReturnValue { get; internal set; }
    }

    public class HandleFakeEventWithoutValidation : IHandleEvent<FakeEventWithoutValidation>
    {
        public void Handle(FakeEventWithoutValidation @event)
        {
            @event.ReturnValue = "faked";
        }
    }
}
