using ScaffoldR.Core.Commands;
using ScaffoldR.Tests.Infrastructure.Commands.Fakes;
using ScaffoldR.Tests.Infrastructure.CompositionRoot;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Commands
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
