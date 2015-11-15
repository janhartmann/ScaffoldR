using System;
using ScaffoldR.Core.Transactions;
using ScaffoldR.Infrastructure.Transactions;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.CompositionRoot.Packages
{
    internal sealed class TransactionPackage : IPackageScaffoldR
    {
        public void RegisterServices(Container container, CompositionRootSettings settings)
        {
            // When there is no explicit transaction processor registered, give it an empty one.
            // The interfaces is most likely to wired up in sub packages for e.g. saving changes to a context.
            var registration = new Lazy<Registration>(() => Lifestyle.Singleton.CreateRegistration<IProcessTransactions, EmptyTransactionProcessor>(container));
            container.ResolveUnregisteredType += (sender, e) => {
                if (e.UnregisteredServiceType == typeof(IProcessTransactions)) e.Register(registration.Value);
            };
        }
    }
}
