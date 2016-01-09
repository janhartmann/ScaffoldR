using System.Reflection;
using ScaffoldR.EntityFramework.Extensions;
using ScaffoldR.EntityFramework.Tests.Fakes;
using ScaffoldR.Infrastructure.CompositionRoot;
using SimpleInjector;
using SimpleInjector.Extensions.LifetimeScoping;
using Xunit;

namespace ScaffoldR.EntityFramework.Tests.CompositionRoot
{
    public class CompositionRootFixture
    {
        public Container Container { get; }

        public CompositionRootFixture()
        {
            Container = new Container();
            Container.Options.DefaultScopedLifestyle = new LifetimeScopeLifestyle();

            var assemblies = new[] { Assembly.GetExecutingAssembly() };

            Container.RegisterScaffoldR(settings =>
            {
                settings.EventAssemblies = assemblies;
                settings.FluentValidationAssemblies = assemblies;
                settings.TransactionAssemblies = assemblies;
                settings.ViewModelAssemblies = assemblies;
            });

            // Register Entity Framework
            Container.RegisterEntityFramework<FakeDbContext>();
        }

    }

    [CollectionDefinition("Simple Injector Tests")]
    public class SimpleInjectorTests : ICollectionFixture<CompositionRootFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

}
