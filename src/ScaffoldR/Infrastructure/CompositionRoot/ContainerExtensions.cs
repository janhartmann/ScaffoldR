using System;
using System.Linq;
using System.Linq.Expressions;
using ScaffoldR.Core.Extensions;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.CompositionRoot
{
    /// <summary>
    /// The main composition root
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Registers ScaffoldR into the Simple Injector container
        /// </summary>
        /// <param name="container">The Simple Injector Container object</param>
        /// <param name="compositionRootSettings">The Composition Root Settings</param>
        public static void RegisterScaffoldR(this Container container, Action<CompositionRootSettings> compositionRootSettings)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            if (compositionRootSettings == null)
                throw new ArgumentNullException(nameof(compositionRootSettings));

            var provider = new CompositionRootSettings();
            compositionRootSettings(provider);

            container.Options.AllowResolvingFuncFactories();
            container.RegisterScaffoldRPackages(provider);
        }

        internal static void RegisterScaffoldRPackages(this Container container, CompositionRootSettings settings)
        {
            var packages = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                           from type in assembly.GetSafeTypes()
                            where typeof(IPackageScaffoldR).IsAssignableFrom(type)
                            where !type.IsAbstract
                            select (IPackageScaffoldR)Activator.CreateInstance(type);

            packages.ToList().ForEach(p => p.RegisterServices(container, settings));
        }

        internal static void AllowResolvingFuncFactories(this ContainerOptions options)
        {
            options.Container.ResolveUnregisteredType += (s, e) => {
                var type = e.UnregisteredServiceType;

                if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Func<>)) return;

                var serviceType = type.GetGenericArguments().First();
                var registration =  options.Container.GetRegistration(serviceType, true);
                var funcType = typeof(Func<>).MakeGenericType(serviceType);

                var factoryDelegate = Expression.Lambda(funcType, registration.BuildExpression()).Compile();
                e.Register(Expression.Constant(factoryDelegate));
            };
        }
    }
}
