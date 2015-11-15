using System;
using System.Diagnostics;
using ScaffoldR.Core.Transactions;

namespace ScaffoldR.Infrastructure.Transactions
{
    internal sealed class QueryNotNullDecorator<TQuery, TResult> : IHandleQuery<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly Func<IHandleQuery<TQuery, TResult>> _handlerFactory;

        public QueryNotNullDecorator(Func<IHandleQuery<TQuery, TResult>> handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        [DebuggerStepThrough]
        public TResult Handle(TQuery query)
        {
            if (Equals(query, null)) throw new ArgumentNullException(nameof(query));
            return _handlerFactory().Handle(query);
        }
    }
}