using ScaffoldR.Core.Events;

namespace ScaffoldR.Core.Transactions
{
    // ReSharper disable UnusedTypeParameter
    /// <summary>
    /// Specifices that the target class is a query with the return type of <typeparamref name="TResult"/>. This is a marker interface and has no methods.
    /// </summary>
    /// <typeparam name="TResult">The return type of the query.</typeparam>
    public interface IQuery<TResult> : IEvent
    {
        // Marker interface
    }
    // ReSharper restore UnusedTypeParameter
}