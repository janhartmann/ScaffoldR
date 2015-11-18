using ScaffoldR.Core.Queries;
using ScaffoldR.Tests.Infrastructure.CompositionRoot;
using ScaffoldR.Tests.Infrastructure.Queries.Fakes;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Queries
{
    [Collection("Simple Injector Tests")]
    public class QueryProcessorTests
    {
        private readonly CompositionRootFixture _fixture;

        public QueryProcessorTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Execute_InvokesQueryHandler_UsingContainerForResolution()
        {
            var queries = _fixture.Container.GetInstance<IProcessQueries>();
            var result = queries.Execute(new FakeQueryWithoutValidator());

            Assert.Equal("faked", result);
        }
    }
}
