using ScaffoldR.Core.Events;
using ScaffoldR.Infrastructure.Events;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.CompositionRoot.Packages
{
    internal sealed class EventPackage : IPackageScaffoldR
    {
        public void RegisterServices(Container container, CompositionRootSettings settings)
        {
            if (settings.EventAssemblies == null) return;

            container.RegisterSingleton<IProcessEvents, EventProcessor>();
             
            container.RegisterCollection(typeof(IHandleEvent<>), settings.EventAssemblies);
           
            container.Register(typeof(ITriggerEvent<>), typeof(MultipleDispatchEventTrigger<>));

            container.RegisterDecorator(
                typeof(ITriggerEvent<>),
                typeof(TriggerEventWhenHandlersExistDecorator<>)
            );
        }
    }
}
