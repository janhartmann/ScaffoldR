using FluentValidation;
using ScaffoldR.Core.Queries;

namespace ScaffoldR.Tests.Infrastructure.Queries.Fakes
{
    public class FakeQueryWithValidator : IQuery<string>
    {
        public string InputValue { get; set; }
    }

    public class ValidateFakeQueryWithValidator: AbstractValidator<FakeQueryWithValidator>
    {
        public ValidateFakeQueryWithValidator()
        {
            RuleFor(x => x.InputValue).NotEmpty();
        }
    }

    public class HandleFakeQueryWithValidator : IHandleQuery<FakeQueryWithValidator, string>
    {
        public string Handle(FakeQueryWithValidator query)
        {
            return "faked";
        }
    }
}
