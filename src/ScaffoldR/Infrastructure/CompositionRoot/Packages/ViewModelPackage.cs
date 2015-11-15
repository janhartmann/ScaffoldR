using ScaffoldR.Core.Views;
using ScaffoldR.Infrastructure.Views;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.CompositionRoot.Packages
{
    internal sealed class ViewModelPackage : IPackageScaffoldR
    {
        public void RegisterServices(Container container, CompositionRootSettings settings)
        {
            if (settings.ViewModelAssemblies == null) return;
     
            container.RegisterSingleton<IProcessViewModels, ViewModelProcessor>();

            container.Register(typeof(IHandleViewModel<>), settings.ViewModelAssemblies);
            container.Register(typeof(IHandleViewModel<,>), settings.ViewModelAssemblies);

            container.RegisterDecorator(
                typeof(IHandleViewModel<>),
                typeof(ViewModelLifetimeScopeDecorator<>),
                Lifestyle.Singleton
            );

            container.RegisterDecorator(
                typeof(IHandleViewModel<,>),
                typeof(ViewModelWithArgumentLifetimeScopeDecorator<,>),
                Lifestyle.Singleton
            );

            container.RegisterDecorator(
                 typeof(IHandleViewModel<,>),
                 typeof(ViewModelWithArgumentNotNullDecorator<,>),
                Lifestyle.Singleton
            );
        }
    }
}
