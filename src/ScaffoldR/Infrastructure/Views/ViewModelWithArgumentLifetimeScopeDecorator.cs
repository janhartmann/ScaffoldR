using System;
using System.Diagnostics;
using ScaffoldR.Core.Views;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.Views
{
    internal sealed class ViewModelWithArgumentLifetimeScopeDecorator<TViewModel, TInput> : IHandleViewModel<TViewModel, TInput> where TViewModel : IViewModel
    {
        private readonly Container _container;
        private readonly Func<IHandleViewModel<TViewModel, TInput>> _handlerFactory;

        public ViewModelWithArgumentLifetimeScopeDecorator(Container container, Func<IHandleViewModel<TViewModel, TInput>> handlerFactory)
        {
            _container = container;
            _handlerFactory = handlerFactory;
        }

        [DebuggerStepThrough]
        public TViewModel Handle(TInput input)
        {
            if (_container.GetCurrentLifetimeScope() != null)
            {
                return _handlerFactory().Handle(input);
            }

            using (_container.BeginLifetimeScope())
            {
                return _handlerFactory().Handle(input);
            }
        }
    }
}
