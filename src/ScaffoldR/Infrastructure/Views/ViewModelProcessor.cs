using ScaffoldR.Core.Views;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.Views
{
    internal sealed class ViewModelProcessor : IProcessViewModels
    {
        private readonly Container _container;

        public ViewModelProcessor(Container container)
        {
            _container = container;
        }

        public TViewModel Create<TViewModel>() where TViewModel : IViewModel
        {
            var handler = _container.GetInstance<IHandleViewModel<TViewModel>>();
            return handler.Handle();
        }

        public TViewModel Create<TViewModel, TInput>(TInput input) where TViewModel : IViewModel
        {
            var handlerType = typeof(IHandleViewModel<,>).MakeGenericType(typeof(TViewModel), typeof(TInput));
            dynamic handler = _container.GetInstance(handlerType);
            return handler.Handle((dynamic)input);
        }
    }
}
