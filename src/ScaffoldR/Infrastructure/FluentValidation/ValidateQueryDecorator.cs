using System.Diagnostics;
using FluentValidation;
using ScaffoldR.Core.Transactions;

namespace ScaffoldR.Infrastructure.FluentValidation
{
    internal sealed class ValidateQueryDecorator<TQuery, TResult> : IHandleQuery<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IHandleQuery<TQuery, TResult> _decorated;
        private readonly IValidator<TQuery> _validator;

        public ValidateQueryDecorator(IHandleQuery<TQuery, TResult> decorated
            , IValidator<TQuery> validator
        )
        {
            _decorated = decorated;
            _validator = validator;
        }

        [DebuggerStepThrough]
        public TResult Handle(TQuery query)
        {
            _validator.ValidateAndThrow(query);

            return _decorated.Handle(query);
        }
    }
}
