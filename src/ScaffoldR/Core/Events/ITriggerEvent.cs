namespace ScaffoldR.Core.Events
{
    public interface ITriggerEvent<in TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// Triggers the event handler of <typeparamref name="TEvent"/>.
        /// </summary>
        /// <param name="evt">The event which should be triggered.</param>
        void Trigger(TEvent evt);
    }
}
