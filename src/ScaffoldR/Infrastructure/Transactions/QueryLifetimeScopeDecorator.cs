using System;
using System.Diagnostics;
using ScaffoldR.Core.Transactions;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.Transactions
{
    internal sealed class QueryLifetimeScopeDecorator<TQuery, TResult> : IHandleQuery<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly Container _container;
        private readonly Func<IHandleQuery<TQuery, TResult>> _handlerFactory;

        public QueryLifetimeScopeDecorator(Container container, Func<IHandleQuery<TQuery, TResult>> handlerFactory)
        {
            _container = container;
            _handlerFactory = handlerFactory;
        }

        [DebuggerStepThrough]
        public TResult Handle(TQuery query)
        {
            if (_container.GetCurrentLifetimeScope() != null)
                return _handlerFactory().Handle(query);
            using (_container.BeginLifetimeScope())
                return _handlerFactory().Handle(query);
        }
    }
}
