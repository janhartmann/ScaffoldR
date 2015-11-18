using ScaffoldR.Core.Commands;

namespace ScaffoldR.Tests.Infrastructure.Commands.Fakes
{
    public class FakeCommandWithoutValidator : ICommand
    {
        public string ReturnValue { get; internal set; }
    }

    public class HandleFakeCommandWithoutValidator : IHandleCommand<FakeCommandWithoutValidator>
    {
        public void Handle(FakeCommandWithoutValidator command)
        {
            command.ReturnValue = "faked";
        }
    }
}
