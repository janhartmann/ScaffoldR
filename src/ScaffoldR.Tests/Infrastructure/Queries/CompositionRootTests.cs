using ScaffoldR.Core.Queries;
using ScaffoldR.Infrastructure.Queries;
using ScaffoldR.Tests.Infrastructure.CompositionRoot;
using SimpleInjector;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Queries
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
    }
}
