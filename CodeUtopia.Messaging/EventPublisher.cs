namespace CodeUtopia.Messaging
{
    public sealed class InProcEventPublisher : IEventPublisher
    {
        private readonly IEventHandlerResolver _eventHandlerResolver;

        public InProcEventPublisher(IEventHandlerResolver eventHandlerResolver)
        {
            _eventHandlerResolver = eventHandlerResolver;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : class
        {
            var eventHandlers = _eventHandlerResolver.Resolve<TEvent>();

            foreach (var eventHandler in eventHandlers)
            {
                eventHandler.Handle(@event);
            }
        }

    }
}