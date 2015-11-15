namespace ScaffoldR.Core.Events
{
    /// <summary>
    /// Handles the <typeparamref name="TEvent"/>.
    /// </summary>
    /// <typeparam name="TEvent">The event which should be handled.</typeparam>
    public interface IHandleEvent<in TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// Handles the <typeparamref name="TEvent"/>.
        /// </summary>
        /// <param name="evt">The <typeparamref name="TEvent"/> which should be handled.</param>
        /// <returns></returns>
        void Handle(TEvent evt);
    }
}
