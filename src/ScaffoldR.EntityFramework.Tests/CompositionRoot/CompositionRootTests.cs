using System.Linq;
using SimpleInjector.Diagnostics;
using Xunit;

namespace ScaffoldR.EntityFramework.Tests.CompositionRoot
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
        public void CompositionRoot_ComposesVerifiedRoot_WithoutDiagnosticsWarnings()
        {
            _fixture.Container.Verify();
            var results = Analyzer.Analyze(_fixture.Container);

            Assert.False(results.Any());
        }

        [Fact]
        public void CompositionRoot_AllowsOverridingRegistrations()
        {
            Assert.False(_fixture.Container.Options.AllowOverridingRegistrations);
        }

    }
}
