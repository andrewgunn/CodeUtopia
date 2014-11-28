using System;
using System.Collections.Concurrent;
using System.Reflection.Emit;
using System.Threading;
using EasyNetQ;
using EasyNetQ.Consumer;

namespace CodeUtopia.Messaging.EasyNetQ
{
    public class EasyNetQBus : IBus
    {
        public EasyNetQBus(global::EasyNetQ.IBus bus,
                           ICommandHandlerResolver commandHandlerResolver,
                           ICommandSender commandSender,
                           IEventCoordinator eventCoordinator,
                           IEventPublisher eventPublisher,
                           string endpointName)
        {
            _bus = bus;
            _commandHandlerResolver = commandHandlerResolver;
            _commandSender = commandSender;
            _eventCoordinator = eventCoordinator;
            _eventPublisher = eventPublisher;
            _endpointName = endpointName;

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

        public void Defer<TMessage>(TMessage message, TimeSpan delay) where TMessage : class
        {
            if (typeof(TMessage).Name.EndsWith("Command"))
            {
                DeferCommand(message, delay);
            }
            else if (typeof(TMessage).Name.EndsWith("Event"))
            {
                DeferEvent(message, delay);
            }
            else
            {
                throw new UnexpectedMessageException(typeof(TMessage));
            }
        }

        private void DeferCommand<TCommand>(TCommand command, TimeSpan delay) where TCommand : class
        {
            Thread.Sleep(delay);
            string queue = _endpointName;

            _bus.Send(queue, command);
        }

        private void DeferEvent<TEvent>(TEvent @event, TimeSpan delay) where TEvent : class
        {
            Thread.Sleep(delay);

            var eventType = typeof(TEvent);

            var subscriptionId = string.Format("{0}-{1}Subscription", _endpointName, eventType.Name);

            _bus.Send(subscriptionId, @event);
        }

        public void Listen<TCommand>() where TCommand : class
        {
            string queueName = _endpointName;

            if (_registration == null)
            {
                lock (_registrationLock)
                {
                    if (_registration == null)
                    {
                        _bus.Receive(queueName,
                            registration =>
                            {
                                _registration = registration;
                            });
                    }
                }
            }

            InternalListen<TCommand>();
        }
    

        private void InternalListen<TCommand>() where TCommand : class
        {
            _registration.Add((TCommand command) =>
                             {
                                 var commandHandler = _commandHandlerResolver.Resolve<TCommand>();

                                 commandHandler.Handle(command);
                             });
        }

        public void Publish<TEvent>(TEvent message) where TEvent : class
        {
            _events.Enqueue(message);
        }

        private void PublishCore(object @event)
        {
            _eventPublisher.Publish((dynamic)@event);
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
            // TODO Make this asynchronous.
            _commands.Enqueue(message);
        }

        private void SendCore(object command)
        {
            // TODO Make this asynchronous.
            _commandSender.Send((dynamic)command);
        }

        public void Subscribe<TEvent>() where TEvent : class
        {
            var eventType = typeof(TEvent);

            var subscriptionId = string.Format("{0}-{1}Subscription", _endpointName, eventType.Name);

            _bus.Subscribe(subscriptionId, (TEvent x) => _eventCoordinator.Coordinate(x, this));
        }

        private readonly global::EasyNetQ.IBus _bus;

        private readonly ICommandHandlerResolver _commandHandlerResolver;

        private readonly ICommandSender _commandSender;

        private ConcurrentQueue<object> _commands;

        private readonly IEventCoordinator _eventCoordinator;

        private readonly IEventPublisher _eventPublisher;

        private readonly string _endpointName;

        private ConcurrentQueue<object> _events;
        
        private IReceiveRegistration _registration;

        private object _registrationLock = new object();
    }
}