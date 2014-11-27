using System.Linq;

namespace CodeUtopia.Messaging.EasyNetQ
{
    public class EasyNetQCommandSender : ICommandSender
    {
        public EasyNetQCommandSender(global::EasyNetQ.IBus bus)
        {
            _bus = bus;
        }

        public void Send<TCommand>(TCommand command) where TCommand : class
        {
            var queueName = typeof (TCommand).Namespace.Split('.').First();

            _bus.Send(queueName, command);
        }

        private readonly global::EasyNetQ.IBus _bus;
    }
}