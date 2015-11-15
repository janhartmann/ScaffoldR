using System;
using System.Data.Entity;
using ScaffoldR.Core.Entities;
using ScaffoldR.Core.Transactions;
using ScaffoldR.EntityFramework.Entities;
using SimpleInjector;

namespace ScaffoldR.EntityFramework.Extensions
{
    /// <summary>
    /// Simple Injector container extensions
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Registers the ScaffoldR Entity Framework integration package into the Simple Injector container
        /// </summary>
        /// <typeparam name="TContext">The DbContext of your application</typeparam>
        /// <param name="container">The Simple Injector container object</param>
        public static void RegisterEntityFramework<TContext>(this Container container) where TContext : DbContext
        {
            if (container == null) 
                throw new ArgumentNullException(nameof(container));

            container.Register<DbContext, TContext>(Lifestyle.Scoped);
            container.Register<IProcessTransactions, EntityFrameworkUnitOfWork>(Lifestyle.Scoped);
            container.Register(typeof(EntityFrameworkRepository<>), typeof(EntityFrameworkRepository<>), Lifestyle.Scoped);
            container.Register(typeof(IEntityWriter<>), typeof(EntityWriterAdapter<>));
            container.Register(typeof(IEntityReader<>), typeof(EntityReaderAdapter<>));
        }

    }
}
