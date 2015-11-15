using FluentValidation;
using ScaffoldR.Core.Validation;
using ScaffoldR.Infrastructure.FluentValidation;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.CompositionRoot.Packages
{
    internal sealed class ValidationPackage : IPackageScaffoldR
    {
        public void RegisterServices(Container container, CompositionRootSettings settings)
        {
            if (settings.FluentValidationAssemblies == null) return;

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            container.RegisterSingleton<IValidatorFactory, SimpleInjectorValidatorFactory>();
            container.RegisterSingleton<IProcessValidation, ValidationProcessor>();
            
            container.Register(typeof(IValidator<>), settings.FluentValidationAssemblies);

            // Add unregistered type resolution for objects missing an IValidator<T>
            container.RegisterConditional(typeof(IValidator<>), typeof(ValidateNothingDecorator<>), Lifestyle.Singleton, context => !context.Handled);
        }
    }
}
