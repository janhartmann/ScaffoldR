using ScaffoldR.Core.Transactions;
using ScaffoldR.Infrastructure.Transactions;
using ScaffoldR.Tests.Infrastructure.CompositionRoot;
using SimpleInjector;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Transactions
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
        public void RegistersIProcessQueries_UsingQueryProcessor_AsSingleton()
        {
            var instance = _fixture.Container.GetInstance<IProcessQueries>();
            var registration = _fixture.Container.GetRegistration(typeof (IProcessQueries));

            Assert.NotNull(instance);
            Assert.IsType<QueryProcessor>(instance);
            Assert.Equal(Lifestyle.Singleton, registration.Lifestyle);
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
