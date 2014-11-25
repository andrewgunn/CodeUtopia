namespace CodeUtopia.Messaging
{
    public sealed class EventDispatcher : IEventDispatcher
    {
        public EventDispatcher(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : class
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