using System;
using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using ScaffoldR.Core.Queries;
using ScaffoldR.Core.Transactions;
using ScaffoldR.Infrastructure.FluentValidation;
using ScaffoldR.Tests.Infrastructure.Queries.Fakes;
using Xunit;

namespace ScaffoldR.Tests.Infrastructure.FluentValidation
{
    public class ValidateQueryDecoratorTests
    {
        [Fact]
        public void Handle_ThrowsValidationException_AndDoesNotInvokeDecoratedHandle_WhenValidationFails()
        {
            var query = new FakeQueryWithoutValidator();
            var decorated = new Mock<IHandleQuery<FakeQueryWithoutValidator, string>>(MockBehavior.Strict);
            var validator = new Mock<IValidator<FakeQueryWithoutValidator>>(MockBehavior.Strict);
            Expression<Func<FakeQueryWithoutValidator, bool>> expectedQuery = x => ReferenceEquals(x, query);
            
            var expectedResult = new ValidationResult(new[] { new ValidationFailure("Name", "Invalid."), });
            validator.Setup(x => x.Validate(It.Is(expectedQuery))).Returns(expectedResult);
            
            var decorator = new ValidateQueryDecorator<FakeQueryWithoutValidator, string>(decorated.Object, validator.Object);
            var exception = Assert.Throws<ValidationException>(() => decorator.Handle(query));

            Assert.NotNull(exception);
            validator.Verify(x => x.Validate(It.Is(expectedQuery)), Times.Once);
            decorated.Verify(x => x.Handle(It.IsAny<FakeQueryWithoutValidator>()), Times.Never);
        }

        [Fact]
        public void Handle_InvokesDecoratedHandle_WhenValidationPasses()
        {
            var query = new FakeQueryWithoutValidator();
            var decorated = new Mock<IHandleQuery<FakeQueryWithoutValidator, string>>(MockBehavior.Strict);
            var validator = new Mock<IValidator<FakeQueryWithoutValidator>>(MockBehavior.Strict);
            var expectedResult = new ValidationResult();
            validator.Setup(x => x.Validate(query)).Returns(expectedResult);
            decorated.Setup(x => x.Handle(query)).Returns("faked");
           
            var decorator = new ValidateQueryDecorator<FakeQueryWithoutValidator, string>(decorated.Object, validator.Object);
            var result = decorator.Handle(query);
            
            Assert.Equal("faked", result);
            validator.Verify(x => x.Validate(query), Times.Once);
            decorated.Verify(x => x.Handle(query), Times.Once);
        }
    }
}
