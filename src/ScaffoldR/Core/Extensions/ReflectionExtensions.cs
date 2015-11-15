using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ScaffoldR.Core.Extensions
{
    /// <summary>
    /// Reflection helper extensions
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Test to see if the type is generically assignable from the another type
        /// </summary>
        /// <param name="openGeneric">A class/interface type</param>
        /// <param name="closedGeneric">A class/interface typ</param>
        /// <returns>True if the type is generically assignable from the other type - otherwise false</returns>
        public static bool IsGenericallyAssignableFrom(this Type openGeneric, Type closedGeneric)
        {
            var interfaceTypes = closedGeneric.GetInterfaces();

            if (interfaceTypes.Where(interfaceType => interfaceType.IsGenericType).Any(interfaceType => interfaceType.GetGenericTypeDefinition() == openGeneric))
            {
                return true;
            }

            var baseType = closedGeneric.BaseType;
            if (baseType == null) return false;

            return baseType.IsGenericType &&
                (baseType.GetGenericTypeDefinition() == openGeneric ||
                openGeneric.IsGenericallyAssignableFrom(baseType));
        }

        /// <summary>
        /// Get types of the assembly, but only returns assembly where the types can be loaded.
        /// </summary>
        /// <param name="assembly">The assembly to look at.</param>
        /// <returns>An array of types.</returns>
        public static Type[] GetSafeTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types;
            }
        }

        /// <summary>
        /// Gets a runtime added attribute to a type.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute</typeparam>
        /// <param name="type">The type.</param>
        /// <returns>The first attribute or null if none is found.</returns>
        public static TAttribute GetRuntimeAddedAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var attributes = TypeDescriptor.GetAttributes(type).OfType<TAttribute>();
            var enumerable = attributes as TAttribute[] ?? attributes.ToArray();
            return enumerable.Any() ? enumerable.First() : null;
        }
    }
}
