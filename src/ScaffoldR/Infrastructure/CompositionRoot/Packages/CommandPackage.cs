using System.Reflection;
using ScaffoldR.Core.Commands;
using ScaffoldR.Core.Events;
using ScaffoldR.Core.Transactions;
using ScaffoldR.Infrastructure.Commands;
using ScaffoldR.Infrastructure.FluentValidation;
using ScaffoldR.Infrastructure.Transactions;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.CompositionRoot.Packages
{
    internal sealed class CommandPackage : IPackageScaffoldR
    {
        public void RegisterServices(Container container, CompositionRootSettings settings)
        {
            if (settings.TransactionAssemblies == null) return;

            container.Register<IProcessCommands, CommandProcessor>(Lifestyle.Singleton);
            container.Register(typeof(IHandleCommand<>), settings.TransactionAssemblies);

            container.RegisterDecorator(
                typeof(IHandleCommand<>),
                typeof(CommandEventProcessingDecorator<>),
                   c =>
                   {
                       var attribute = c.ImplementationType.GetCustomAttribute<RaiseEventAttribute>();
                       return attribute != null && attribute.Enabled;
                   }
            );

            container.RegisterDecorator(typeof(IHandleCommand<>), 
                typeof(CommandTransactionDecorator<>), 
                c => c.ImplementationType.GetCustomAttribute<TransactionalAttribute>() != null
            );

            container.RegisterDecorator(
               typeof(IHandleCommand<>),
               typeof(ValidateCommandDecorator<>)
            );

            container.RegisterDecorator(
                typeof(IHandleCommand<>),
                typeof(CommandLifetimeScopeDecorator<>),
                Lifestyle.Singleton
            );

            container.RegisterDecorator(
                typeof(IHandleCommand<>),
                typeof(CommandNotNullDecorator<>),
                Lifestyle.Singleton
            );
        }
    }
}
