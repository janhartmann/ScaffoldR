using ScaffoldR.Core.Commands;
using ScaffoldR.Infrastructure.Commands;
using ScaffoldR.Tests.Infrastructure.CompositionRoot;
using SimpleInjector;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Commands
{
    [Collection("Simple Injector Tests")]
    public class CompositionRootTests
    {
        private readonly CompositionRootFixture _fixture;

        public CompositionRootTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void RegistersIProcessCommands_UsingCommandProcessor_AsSingleton()
        {
            var instance = _fixture.Container.GetInstance<IProcessCommands>();
            var registration = _fixture.Container.GetRegistration(typeof (IProcessCommands));

            Assert.NotNull(instance);
            Assert.IsType<CommandProcessor>(instance);
            Assert.Equal(Lifestyle.Singleton, registration.Lifestyle);
        }

        
    }
}
