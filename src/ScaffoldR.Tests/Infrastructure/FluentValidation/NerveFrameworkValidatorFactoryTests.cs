using FluentValidation;
using ScaffoldR.Tests.Infrastructure.CompositionRoot;
using ScaffoldR.Tests.Infrastructure.Transactions.Fakes;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.FluentValidation
{
    [Collection("Simple Injector Tests")]
    public class ScaffoldRValidatorFactoryTests
    {
        private readonly CompositionRootFixture _fixture;

        public ScaffoldRValidatorFactoryTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CreateInstance_CanCreateInstanceOf_ValidateNothingValidator()
        {
            var validator = _fixture.Container.GetInstance(typeof(IValidator<FakeCommandWithValidator>));

            Assert.NotNull(validator);
            Assert.IsType<ValidateFakeCommandWithValidator>(validator);
        }
    }
}
