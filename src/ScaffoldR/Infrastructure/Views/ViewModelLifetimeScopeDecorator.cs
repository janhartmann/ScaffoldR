using System;
using System.Diagnostics;
using ScaffoldR.Core.Views;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.Views
{
    internal sealed class ViewModelLifetimeScopeDecorator<TViewModel> : IHandleViewModel<TViewModel> where TViewModel : IViewModel
    {
        private readonly Container _container;
        private readonly Func<IHandleViewModel<TViewModel>> _handlerFactory;

        public ViewModelLifetimeScopeDecorator(Container container, Func<IHandleViewModel<TViewModel>> handlerFactory)
        {
            _container = container;
            _handlerFactory = handlerFactory;
        }

        [DebuggerStepThrough]
        public TViewModel Handle()
        {
            if (_container.GetCurrentLifetimeScope() != null)
            {
                return _handlerFactory().Handle();
            }

            using (_container.BeginLifetimeScope())
            {
                return _handlerFactory().Handle();
            }
        }
    }
}
