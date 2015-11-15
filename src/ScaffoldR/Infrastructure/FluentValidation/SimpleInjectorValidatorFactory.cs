using System;
using FluentValidation;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.FluentValidation
{
    /// <summary>
    /// The Fluent Validation Validator Factory. Used to find instances of the <see cref="AbstractValidator{T}"/>'s.
    /// </summary>
    public class SimpleInjectorValidatorFactory : IValidatorFactory
    {
        private readonly Container _container;

        /// <summary>
        /// The constructor of the factory.
        /// </summary>
        /// <param name="container">The Simple Injector Container</param>
        public SimpleInjectorValidatorFactory(Container container)
        {
            _container = container;
        }

        /// <summary>
        /// Gets the validator for the specified type.
        /// </summary>
        public IValidator<T> GetValidator<T>()
        {
            return _container.GetInstance<IValidator<T>>();
        }

        /// <summary>
        /// Gets the validator for the specified type.
        /// </summary>
        public IValidator GetValidator(Type type)
        {
            var validator = typeof(IValidator<>).MakeGenericType(type);
            return _container.GetInstance(validator) as IValidator;
        }
    }
}
