namespace CodeUtopia.Messaging
{
    public sealed class InProcEventPublisher : IEventPublisher
    {
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

        private readonly IEventHandlerResolver _eventHandlerResolver;
    }
}