using System;
using System.Threading;

namespace CodeUtopia
{
    public class RetryEventHandlerDecorator<TEvent> : IEventHandler<TEvent>
        where TEvent : class
    {
        public RetryEventHandlerDecorator(IEventHandler<TEvent> decorated)
        {
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
                Thread.Sleep(1000);

                _decorated.Handle(@event);
            }
        }

        private readonly IEventHandler<TEvent> _decorated;
    }
}