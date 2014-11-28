using System;
using CodeUtopia.Messaging;

namespace CodeUtopia
{
    public class RetryCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : class
    {
        static RetryCommandHandlerDecorator()
        {
            _retryCounts = new Counter();
        }

        public RetryCommandHandlerDecorator(IBus bus, ICommandHandler<TCommand> decorated)
        {
            _bus = bus;
            _decorated = decorated;
        }

        public void Handle(TCommand command)
        {
            try
            {
                _decorated.Handle(command);
            }
            catch (Exception)
            {
                _retryCounts.Increment();

                Console.WriteLine("Retrying ({2})...\t{0} ({1})", command, _decorated, _retryCounts.Count);

                _bus.Defer(command, TimeSpan.FromSeconds(1));
            }
        }

        private readonly IBus _bus;

        private readonly ICommandHandler<TCommand> _decorated;

        private static readonly Counter _retryCounts;
    }
}