using System;

namespace CodeUtopia.Messaging
{
    public class LoggingCommandSenderDecorator : ICommandSender
    {
        public LoggingCommandSenderDecorator(ICommandSender decorated)
        {
            _decorated = decorated;
        }

        public void Send<T>(T message) where T : class
        {
            Console.WriteLine("Sending...\t{0}", message);

            _decorated.Send(message);
        }

        private readonly ICommandSender _decorated;
    }
}