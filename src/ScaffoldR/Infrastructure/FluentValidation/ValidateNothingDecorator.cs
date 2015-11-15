using FluentValidation;

namespace ScaffoldR.Infrastructure.FluentValidation
{
    /// <summary>
    /// Adds an unregistered type resolution for objects missing an IValidator.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    internal sealed class ValidateNothingDecorator<T> : AbstractValidator<T>
    {
      
    }
}
