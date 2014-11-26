namespace CodeUtopia.Messaging.EasyNetQ
{
    public class EasyNetQEventPublisher : IEventPublisher
    {
        private readonly global::EasyNetQ.IBus _bus;

        public EasyNetQEventPublisher(global::EasyNetQ.IBus bus)
        {
            _bus = bus;
        }

        public void Publish<TEvent>(TEvent message) where TEvent : class
        {
            _bus.Publish(message);
        }
    }
}
