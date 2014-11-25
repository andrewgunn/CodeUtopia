namespace CodeUtopia.Messaging
{
    public sealed class EventPublisher : IEventPublisher
    {
        public EventPublisher(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : class
        {
            var eventHandlers = _dependencyResolver.Resolve<IEventHandler<TEvent>[]>();

            foreach (var eventHandler in eventHandlers)
            {
                eventHandler.Handle(@event);
            }
        }

        private readonly IDependencyResolver _dependencyResolver;
    }
}