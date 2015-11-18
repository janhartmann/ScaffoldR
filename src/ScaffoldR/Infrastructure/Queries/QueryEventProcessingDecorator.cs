using System;
using System.Diagnostics;
using ScaffoldR.Core.Events;
using ScaffoldR.Core.Extensions;
using ScaffoldR.Core.Queries;

namespace ScaffoldR.Infrastructure.Queries
{
    internal sealed class QueryEventProcessingDecorator<TQuery, TResult> : IHandleQuery<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IProcessEvents _events;
        private readonly Func<IHandleQuery<TQuery, TResult>> _handlerFactory;
        
        public QueryEventProcessingDecorator(IProcessEvents events, Func<IHandleQuery<TQuery, TResult>> handlerFactory)
        {
            _events = events;
            _handlerFactory = handlerFactory;
        }

        [DebuggerStepThrough]
        public TResult Handle(TQuery query)
        {
            var handler = _handlerFactory();
            try
            {
                return handler.Handle(query);
            }
            finally
            {
                var attribute = handler.GetType().GetRuntimeAddedAttribute<RaiseEventAttribute>();
                if (attribute != null && attribute.Enabled)
                {
                    _events.Raise(query);
                }
            }
        }

    }
}
