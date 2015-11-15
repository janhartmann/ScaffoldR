using System;
using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using ScaffoldR.Core.Transactions;
using ScaffoldR.Infrastructure.FluentValidation;
using ScaffoldR.Tests.Infrastructure.Transactions.Fakes;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.FluentValidation
{
    public class ValidateCommandDecoratorTests
    {
        [Fact]
        public void Handle_ThrowsValidationException_AndDoesNotInvokeDecoratedHandle_WhenValidationFails()
        {
            var command = new FakeCommandWithValidator();
            var decorated = new Mock<IHandleCommand<FakeCommandWithValidator>>(MockBehavior.Strict);
            var validator = new Mock<IValidator<FakeCommandWithValidator>>(MockBehavior.Strict);
            Expression<Func<FakeCommandWithValidator, bool>> expectedCommand = x => ReferenceEquals(x, command);
            
            var expectedResult = new ValidationResult(new[] { new ValidationFailure("Name", "Invalid.", command.ReturnValue), });
            validator.Setup(x => x.Validate(It.Is(expectedCommand))).Returns(expectedResult);
            var decorator = new ValidateCommandDecorator<FakeCommandWithValidator>(decorated.Object, validator.Object);
            var exception = Assert.Throws<ValidationException>(() => decorator.Handle(command));

            Assert.NotNull(exception);
            validator.Verify(x => x.Validate(It.Is(expectedCommand)), Times.Once);
            decorated.Verify(x => x.Handle(It.IsAny<FakeCommandWithValidator>()), Times.Never);
        }

        [Fact]
        public void Handle_InvokesDecoratedHandle_WhenValidationPasses()
        {
            var command = new FakeCommandWithValidator();
            var decorated = new Mock<IHandleCommand<FakeCommandWithValidator>>(MockBehavior.Strict);
            var validator = new Mock<IValidator<FakeCommandWithValidator>>(MockBehavior.Strict);
            Expression<Func<FakeCommandWithValidator, bool>> expectedCommand = x => ReferenceEquals(x, command);
            
            var expectedResult = new ValidationResult();
            validator.Setup(x => x.Validate(It.Is(expectedCommand))).Returns(expectedResult);
            decorated.Setup(x => x.Handle(It.Is(expectedCommand)));
            
            var decorator = new ValidateCommandDecorator<FakeCommandWithValidator>(decorated.Object, validator.Object);
            decorator.Handle(command);

            validator.Verify(x => x.Validate(It.Is(expectedCommand)), Times.Once);
            decorated.Verify(x => x.Handle(It.Is(expectedCommand)), Times.Once);
        }
    }
}
