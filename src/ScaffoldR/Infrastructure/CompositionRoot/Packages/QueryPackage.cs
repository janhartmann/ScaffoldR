using System.Reflection;
using ScaffoldR.Core.Events;
using ScaffoldR.Core.Queries;
using ScaffoldR.Core.Transactions;
using ScaffoldR.Infrastructure.FluentValidation;
using ScaffoldR.Infrastructure.Queries;
using ScaffoldR.Infrastructure.Transactions;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.CompositionRoot.Packages
{
    internal sealed class QueryPackage : IPackageScaffoldR
    {
        public void RegisterServices(Container container, CompositionRootSettings settings)
        {
            if (settings.TransactionAssemblies == null) return;

            container.Register<IProcessQueries, QueryProcessor>(Lifestyle.Singleton);

            container.Register(typeof (IHandleQuery<,>), settings.TransactionAssemblies);

            container.RegisterDecorator(
                  typeof(IHandleQuery<,>),
                  typeof(QueryEventProcessingDecorator<,>),
                  c => 
                  {
                        var attribute = c.ImplementationType.GetCustomAttribute<RaiseEventAttribute>();
                        return attribute != null && attribute.Enabled;
                  }
            );

            container.RegisterDecorator(
                typeof (IHandleQuery<,>),
                typeof (ValidateQueryDecorator<,>)
                );

            container.RegisterDecorator(
                typeof (IHandleQuery<,>),
                typeof (QueryLifetimeScopeDecorator<,>),
                Lifestyle.Singleton
                );

            container.RegisterDecorator(
                typeof (IHandleQuery<,>),
                typeof (QueryNotNullDecorator<,>),
                Lifestyle.Singleton
                );
        }
    }
}
