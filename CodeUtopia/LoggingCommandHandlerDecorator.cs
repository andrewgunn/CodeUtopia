using System;

namespace CodeUtopia
{
    public class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : class
    {
        public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> decorated)
        {
            _decorated = decorated;
        }

        public void Handle(TCommand command)
        {
            Console.WriteLine("Handling...\t{0}", command);

            _decorated.Handle(command);
        }

        private readonly ICommandHandler<TCommand> _decorated;
    }
}