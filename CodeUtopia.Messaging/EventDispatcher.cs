using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            var tasks = eventHandlers.Select(x => Task.Run(() => x.Handle(@event)))
                                     .ToList();

            Task.WhenAll(tasks).Wait();
        }

        private readonly IDependencyResolver _dependencyResolver;
    }
}