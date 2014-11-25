using System.Collections.Concurrent;

namespace CodeUtopia.Messaging
{
    public class InMemoryBus : IBus
    {
        public InMemoryBus(ICommandSender commandDispatcher, IEventPublisher eventDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _eventDispatcher = eventDispatcher;

            ResetQueues();
        }

        public void Commit()
        {
            foreach (var command in _commands)
            {
                SendCore(command);
            }

            foreach (var @event in _events)
            {
                PublishCore(@event);
            }

            ResetQueues();
        }

        public void Publish<T>(T message) where T : class
        {
            _events.Enqueue(message);
        }

        private void PublishCore(object @event)
        {
            _eventDispatcher.Publish((dynamic)@event);
        }

        private void ResetQueues()
        {
            _commands = new ConcurrentQueue<object>();
            _events = new ConcurrentQueue<object>();
        }

        public void Rollback()
        {
            ResetQueues();
        }

        public void Send<T>(T message) where T : class
        {
            // TODO Make this asynchronous.
            _commands.Enqueue(message);
        }

        private void SendCore(object command)
        {
            // TODO Make this asynchronous.
            _commandDispatcher.Send((dynamic)command);
        }

        private readonly ICommandSender _commandDispatcher;

        private ConcurrentQueue<object> _commands;

        private readonly IEventPublisher _eventDispatcher;

        private ConcurrentQueue<object> _events;
    }
}