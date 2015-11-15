namespace ScaffoldR.Core.Events
{
    /// <summary>
    /// Processes events.
    /// </summary>
    public interface IProcessEvents
    {
        /// <summary>
        /// Triggers the event.
        /// </summary>
        /// <param name="evt">The event to be raised.</param>
        /// <returns>An async Task</returns>
        void Raise(IEvent evt);
    }
}
