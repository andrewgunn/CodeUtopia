namespace CodeUtopia.Messaging
{
    public class EventCoordinator : IEventCoordinator
    {
        public EventCoordinator(IEventHandlerResolver eventHandlerResolver)
        {
            _eventHandlerResolver = eventHandlerResolver;
        }

        public void Coordinate<TEvent>(TEvent @event, IBus bus) where TEvent : class
        {
            var eventHandlers = _eventHandlerResolver.Resolve<TEvent>();

            foreach (var eventHandler in eventHandlers)
            {
                eventHandler.Handle(@event);
            }
        }

        private readonly IEventHandlerResolver _eventHandlerResolver;
    }
}