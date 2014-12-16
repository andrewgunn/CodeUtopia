namespace CodeUtopia.Messaging.NServiceBus
{
    public class NServiceBusCommandSender : ICommandSender
    {
        public NServiceBusCommandSender(global::NServiceBus.IBus bus)
        {
            _bus = bus;
        }

        public void Send<TCommand>(TCommand command) where TCommand : class
        {
            _bus.Send(command);
        }

        private readonly global::NServiceBus.IBus _bus;
    }
}