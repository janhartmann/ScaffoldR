using ScaffoldR.Core.Transactions;

namespace ScaffoldR.Tests.Infrastructure.Transactions.Fakes
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
