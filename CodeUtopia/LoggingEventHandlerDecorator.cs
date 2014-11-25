using System;

namespace CodeUtopia
{
    public class LoggingEventHandlerDecorator<TEvent> : IEventHandler<TEvent>
        where TEvent : class
    {
        public LoggingEventHandlerDecorator(IEventHandler<TEvent> decorated)
        {
            _decorated = decorated;
        }

        public void Handle(TEvent @event)
        {
            Console.WriteLine("Handling...\t{0} ({1})", @event, _decorated);

            _decorated.Handle(@event);
        }

        private readonly IEventHandler<TEvent> _decorated;
    }
}