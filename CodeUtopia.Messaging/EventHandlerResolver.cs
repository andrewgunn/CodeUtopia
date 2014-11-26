namespace CodeUtopia.Messaging
{
    public class EventHandlerResolver : IEventHandlerResolver
    {
        public EventHandlerResolver(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public IEventHandler<TEvent>[] Resolve<TEvent>() where TEvent : class
        {
            var eventHandlers = _dependencyResolver.Resolve<IEventHandler<TEvent>[]>();

            return eventHandlers;
        }

        private readonly IDependencyResolver _dependencyResolver;
    }
}