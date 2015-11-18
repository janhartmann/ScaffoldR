using FluentValidation;
using ScaffoldR.Core.Commands;

namespace ScaffoldR.Tests.Infrastructure.Commands.Fakes
{
    public class FakeCommandWithValidator : ICommand
    {
        public string InputValue { get; set; }
        public string ReturnValue { get; internal set; }
    }

    public class ValidateFakeCommandWithValidator : AbstractValidator<FakeCommandWithValidator>
    {
        public ValidateFakeCommandWithValidator()
        {
            RuleFor(x => x.InputValue).NotEmpty();
        }
    }

    public class HandleFakeCommandWithValidator : IHandleCommand<FakeCommandWithValidator>
    {
        public void Handle(FakeCommandWithValidator command)
        {
            command.ReturnValue = "faked";
        }
    }
}
