using ScaffoldR.Core.Transactions;
using ScaffoldR.Tests.Infrastructure.CompositionRoot;
using ScaffoldR.Tests.Infrastructure.Transactions.Fakes;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Transactions
{
    [Collection("Simple Injector Tests")]
    public class CommandProcessorTests
    {
        private readonly CompositionRootFixture _fixture;

        public CommandProcessorTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Execute_InvokesCommandHandler_UsingContainerForResolution()
        {
            var commands = _fixture.Container.GetInstance<IProcessCommands>();
            var command = new FakeCommandWithoutValidator();
            commands.Execute(command);

            Assert.Equal("faked", command.ReturnValue);

        }

    }
}
