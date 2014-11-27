using System;
using System.Threading;
using CodeUtopia.Messaging;

namespace CodeUtopia
{
    public class RetryEventHandlerDecorator<TEvent> : IEventHandler<TEvent>
        where TEvent : class
    {
        static RetryEventHandlerDecorator()
        {
            _retryCounts = new Counter();
        }

        public RetryEventHandlerDecorator(IBus bus, IEventHandler<TEvent> decorated)
        {
            _bus = bus;
            _decorated = decorated;
        }

        public void Handle(TEvent @event)
        {
            Console.WriteLine("Retrying...\t{0} ({1})", @event, _decorated);

            try
            {
                _decorated.Handle(@event);
            }
            catch (Exception)
            {
                _retryCounts.Increment();

                Console.WriteLine("Retrying ({2})...\t{0} ({1})", @event, _decorated, _retryCounts);

                _bus.Defer(@event, TimeSpan.FromSeconds(1));
            }
        }

        private readonly IBus _bus;

        private readonly IEventHandler<TEvent> _decorated;

        private static readonly Counter _retryCounts;
    }
}