using System;
using System.Collections.Concurrent;
using System.Threading;

namespace CodeUtopia.Messaging.NServiceBus
{
    public class NServiceBusBus : IBus
    {
        public NServiceBusBus(global::NServiceBus.IBus bus)
        {
            _bus = bus;

            ResetQueues();
        }

        public void Commit()
        {
            foreach (var command in _commands)
            {
                _bus.Send(command);
            }

            foreach (var @event in _events)
            {
                _bus.Publish(@event);
            }

            ResetQueues();
        }

        public void Defer<TMessage>(TMessage message, TimeSpan delay) where TMessage : class
        {
            _bus.Defer(delay, message);
        }
        
        public void Listen<TCommand>() where TCommand : class
        {
           // Dont need this for NServiceBus listens to everything.
        }
    
        public void Publish<TEvent>(TEvent message) where TEvent : class
        {
            _events.Enqueue(message);
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

        public void Send<TCommand>(TCommand message) where TCommand : class
        {
            _commands.Enqueue(message);
        }
        
        public void Subscribe<TEvent>() where TEvent : class
        {
            _bus.Subscribe<TEvent>();
        }

        private readonly global::NServiceBus.IBus _bus;

        private ConcurrentQueue<object> _commands;
        
        private ConcurrentQueue<object> _events;
    }
}