using FluentValidation;
using ScaffoldR.Core.Validation;
using ScaffoldR.Infrastructure.FluentValidation;
using ScaffoldR.Tests.Infrastructure.Commands.Fakes;
using ScaffoldR.Tests.Infrastructure.CompositionRoot;
using SimpleInjector;
using Xunit;
using CascadeMode = FluentValidation.CascadeMode;

namespace ScaffoldR.Tests.Infrastructure.FluentValidation
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
        public void Sets_ValidatorOptions_CascadeMode_To_StopOnFirstFailure()
        {
            Assert.Equal(CascadeMode.StopOnFirstFailure, ValidatorOptions.CascadeMode);
        }

        [Fact]
        public void RegistersIProcessValidation_UsingValidationProcessor_AsSingleton()
        {
            var instance = _fixture.Container.GetInstance<IProcessValidation>();
            var registration = _fixture.Container.GetRegistration(typeof(IProcessValidation));

            Assert.NotNull(instance);
            Assert.IsType<ValidationProcessor>(instance);
            Assert.Equal(Lifestyle.Singleton, registration.Lifestyle);
        }

        [Fact]
        public void RegistersIValidator_Transiently_UsingOpenGenerics_WhenValidatorExists()
        {
            var instance = _fixture.Container.GetInstance<IValidator<FakeCommandWithValidator>>();
            var registration = _fixture.Container.GetRegistration(typeof(IValidator<FakeCommandWithValidator>));

            Assert.NotNull(instance);
            Assert.IsType<ValidateFakeCommandWithValidator>(instance);
            Assert.Equal(Lifestyle.Transient, registration.Lifestyle);
        }

        [Fact]
        public void RegistersIValidator_AsSingleton_UsingValidateNothingDecorator_WhenValidatorDoesNotExist()
        {
            var instance = _fixture.Container.GetInstance<global::FluentValidation.IValidator<FakeCommandWithoutValidator>>();
            var registration = _fixture.Container.GetRegistration(typeof(IValidator<FakeCommandWithoutValidator>));

            Assert.NotNull(instance);
            Assert.IsType<ValidateNothingDecorator<FakeCommandWithoutValidator>>(instance);
            Assert.Equal(Lifestyle.Singleton, registration.Lifestyle);
        }
    }
}
