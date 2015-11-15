using System;
using ScaffoldR.Core.Views;

namespace ScaffoldR.Infrastructure.Views
{
    internal sealed class ViewModelWithArgumentNotNullDecorator<TViewModel, TInput> : IHandleViewModel<TViewModel, TInput> where TViewModel : IViewModel
    {
        private readonly Func<IHandleViewModel<TViewModel, TInput>> _handlerFactory;

        public ViewModelWithArgumentNotNullDecorator(Func<IHandleViewModel<TViewModel, TInput>> handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        public TViewModel Handle(TInput input)
        {
            if (Equals(input, null)) throw new ArgumentNullException(nameof(input));
            return _handlerFactory().Handle(input);
        }
    }
}