using SimpleInjector;

namespace ScaffoldR.Infrastructure.CompositionRoot
{
    /// <summary>
    /// A ScaffoldR encapsules the ScaffoldR infrastructure and registeres the services in the Simple Injector container.
    /// </summary>
    public interface IPackageScaffoldR
    {
        /// <summary>
        /// Registers the services in the <see cref="Container"/>
        /// </summary>
        /// <param name="container">The Simple Injector container</param>
        /// <param name="settings">The Composition Root Settings</param>
        void RegisterServices(Container container, CompositionRootSettings settings);
    }
}
