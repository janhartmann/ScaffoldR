namespace ScaffoldR.Core.Events
{
    /// <summary>
    /// Triggers events
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface ITriggerEvent<in TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// Triggers the event handler of <typeparamref name="TEvent"/>.
        /// </summary>
        /// <param name="evt">The event which should be triggered.</param>
        void Trigger(TEvent evt);
    }
}
