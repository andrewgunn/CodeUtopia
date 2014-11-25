using System;

namespace CodeUtopia.Messaging
{
    public class LoggingEventPublisherDecorator : IEventPublisher
    {
        public LoggingEventPublisherDecorator(IEventPublisher decorated)
        {
            _decorated = decorated;
        }

        public void Publish<T>(T message) where T : class
        {
            Console.WriteLine("Publishing...\t{0}", message);

            _decorated.Publish(message);
        }

        private readonly IEventPublisher _decorated;
    }
}