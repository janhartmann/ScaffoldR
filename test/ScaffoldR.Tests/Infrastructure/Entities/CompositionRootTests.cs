using ScaffoldR.Core.Transactions;
using ScaffoldR.Infrastructure.Transactions;
using ScaffoldR.Tests.Infrastructure.CompositionRoot;
using SimpleInjector;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Entities
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
        public void RegistersEmptyProcessTransation_AsSingleton_WhenIProcessTransactionsIsNotRegisted()
        {
            var instance = _fixture.Container.GetInstance<IProcessTransactions>();
            var registration = _fixture.Container.GetRegistration(typeof (IProcessTransactions));

            Assert.NotNull(instance);
            Assert.IsType<EmptyTransactionProcessor>(instance);
            Assert.Equal(Lifestyle.Singleton, registration.Lifestyle);
        }
    }
}
