namespace CodeUtopia.Messaging.EasyNetQ
{
    public class EasyNetQEventPublisher : IEventPublisher
    {
        public EasyNetQEventPublisher(global::EasyNetQ.IBus bus)
        {
            _bus = bus;
        }

        public void Publish<TEvent>(TEvent message) where TEvent : class
        {
            _bus.Publish(message);
        }

        private readonly global::EasyNetQ.IBus _bus;
    }
}