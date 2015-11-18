using System.Reflection;
using FluentValidation;
using ScaffoldR.Core.Validation;
using ScaffoldR.Infrastructure.FluentValidation;
using ScaffoldR.Tests.Infrastructure.Commands.Fakes;
using ScaffoldR.Tests.Infrastructure.Queries.Fakes;
using SimpleInjector;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.FluentValidation
{
    public class ValidationProcessorTests
    {
        [Fact]
        public void ValidateCommand_InvokesValidator_UsingContainerForResolution()
        {
            var container = new Container();
            container.RegisterSingleton<IProcessValidation, ValidationProcessor>();
            container.Register(typeof(IValidator<>), new[] { Assembly.GetExecutingAssembly() });
            container.Verify();

            var validation = container.GetInstance<IProcessValidation>();
            var result = validation.Validate(new FakeCommandWithValidator { InputValue = null });

            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
        }

        [Fact]
        public void ValidateQuery_InvokesValidator_UsingContainerForResolution()
        {
            var container = new Container();
            container.RegisterSingleton<IProcessValidation, ValidationProcessor>();
            container.Register(typeof(IValidator<>), new[] { Assembly.GetExecutingAssembly() });
            container.Verify();
            
            var validation = container.GetInstance<IProcessValidation>();
            var result = validation.Validate(new FakeQueryWithValidator { InputValue = null });
            
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
        }
    }
}
