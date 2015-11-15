using ScaffoldR.Core.Events;

namespace ScaffoldR.Core.Transactions
{
    /// <summary>
    /// Specifices that the target class is a command. This is a marker interface and has no methods.
    /// </summary>
    public interface ICommand : IEvent
    {
        // Marker interface
    }
}