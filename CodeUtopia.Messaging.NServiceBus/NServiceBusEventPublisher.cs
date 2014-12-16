namespace CodeUtopia.Messaging.NServiceBus
{
    public class NServiceBusEventPublisher : IEventPublisher
    {
        public NServiceBusEventPublisher(global::NServiceBus.IBus bus)
        {
            _bus = bus;
        }

        public void Publish<TEvent>(TEvent message) where TEvent : class
        {
            _bus.Publish(message);
        }

        private readonly global::NServiceBus.IBus _bus;
    }
}