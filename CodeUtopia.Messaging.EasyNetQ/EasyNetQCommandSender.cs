namespace CodeUtopia.Messaging.EasyNetQ
{
    public class EasyNetQCommandSender : ICommandSender
    {
        private readonly global::EasyNetQ.IBus _bus;

        public EasyNetQCommandSender(global::EasyNetQ.IBus bus)
        {
            _bus = bus;
        }

        public void Send<TCommand>(TCommand command) where TCommand : class
        {
            _bus.Send("MagicQueue", command);
        }
    }
}