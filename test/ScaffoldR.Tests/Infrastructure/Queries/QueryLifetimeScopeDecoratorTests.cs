using Moq;
using ScaffoldR.Core.Queries;
using ScaffoldR.Infrastructure.Queries;
using ScaffoldR.Tests.Infrastructure.CompositionRoot;
using ScaffoldR.Tests.Infrastructure.Queries.Fakes;
using SimpleInjector;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.Queries
{
    [Collection("Simple Injector Tests")]
    public class QueryLifetimeScopeDecoratorTests
    {
        private readonly CompositionRootFixture _fixture;

        public QueryLifetimeScopeDecoratorTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void BeginsLifetimeScope_WhenCurrentLifetimeScope_IsNull()
        {
            var query = new FakeQueryWithoutValidator();
            var decorated = new Mock<IHandleQuery<FakeQueryWithoutValidator, string>>(MockBehavior.Strict);
            decorated.Setup(x => x.Handle(query)).Returns("faked");

            var decorator = new QueryLifetimeScopeDecorator<FakeQueryWithoutValidator, string>(_fixture.Container, () => decorated.Object);
            Assert.Equal(null, _fixture.Container.GetCurrentLifetimeScope());

            var result = decorator.Handle(query);

            Assert.Equal("faked", result);
            decorated.Verify(x => x.Handle(query), Times.Once);
        }

        [Fact]
        public void UsesCurrentLifetimeScope_WhenCurrentLifetimeScope_IsNotNull()
        {
            var query = new FakeQueryWithoutValidator();
            var decorated = new Mock<IHandleQuery<FakeQueryWithoutValidator, string>>(MockBehavior.Strict);
            decorated.Setup(x => x.Handle(query)).Returns("faked");
           
            var decorator = new QueryLifetimeScopeDecorator<FakeQueryWithoutValidator, string>(_fixture.Container, () => decorated.Object);
            Assert.Equal(null, _fixture.Container.GetCurrentLifetimeScope());

            string result;
            using (_fixture.Container.BeginLifetimeScope())
            {
                result = decorator.Handle(query);
            }

            Assert.Equal("faked", result);

            decorated.Verify(x => x.Handle(query), Times.Once);
        }
    }
}
