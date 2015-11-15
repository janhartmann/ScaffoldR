using ScaffoldR.Core.Tasks;
using ScaffoldR.Infrastructure.Tasks;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.CompositionRoot.Packages
{
    internal sealed class TaskPackage : IPackageScaffoldR
    {
        public void RegisterServices(Container container, CompositionRootSettings settings)
        {
           container.Register<ITaskFactory, TaskFactory>(Lifestyle.Singleton);
        }
    }
}
