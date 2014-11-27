using System;
using System.Collections.Concurrent;
using System.Threading;
using EasyNetQ.Consumer;

namespace CodeUtopia.Messaging.EasyNetQ
{
    public class EasyNetQBus : IBus
    {
        public EasyNetQBus(global::EasyNetQ.IBus bus,
                           ICommandHandlerResolver commandHandlerResolver,
                           ICommandSender commandSender,
                           IEventCoordinator eventCoordinator,
                           IEventPublisher eventPublisher)
        {
            _bus = bus;
            _commandHandlerResolver = commandHandlerResolver;
            _commandSender = commandSender;
            _eventCoordinator = eventCoordinator;
            _eventPublisher = eventPublisher;

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
            const string queue = "BankingBackend";

            _bus.Send(queue, command);
        }

        private void DeferEvent<TEvent>(TEvent @event, TimeSpan delay) where TEvent : class
        {
            Thread.Sleep(delay);

            var eventType = typeof(TEvent);

            var subscriptionId = string.Format("BankingManagementClient-{0}Subscription", eventType.Name);

            _bus.Send(subscriptionId, @event);
        }

        public void Listen<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6>() where TCommand1 : class
            where TCommand2 : class where TCommand3 : class where TCommand4 : class where TCommand5 : class
            where TCommand6 : class
        {
            const string queueName = "BankingBackend";

            _bus.Receive(queueName,
                         registration =>
                         {
                             Listen<TCommand1>(registration);
                             Listen<TCommand2>(registration);
                             Listen<TCommand3>(registration);
                             Listen<TCommand4>(registration);
                             Listen<TCommand5>(registration);
                             Listen<TCommand6>(registration);
                         });
        }

        private void Listen<TCommand>(IReceiveRegistration registration) where TCommand : class
        {
            registration.Add((TCommand command) =>
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

            const string endpointName = "BankingManagementClient";
            var subscriptionId = string.Format("{0}-{1}Subscription", endpointName, eventType.Name);

            _bus.Subscribe(subscriptionId, (TEvent x) => _eventCoordinator.Coordinate(x));
        }

        private readonly global::EasyNetQ.IBus _bus;

        private readonly ICommandHandlerResolver _commandHandlerResolver;

        private readonly ICommandSender _commandSender;

        private ConcurrentQueue<object> _commands;

        private readonly IEventCoordinator _eventCoordinator;

        private readonly IEventPublisher _eventPublisher;

        private ConcurrentQueue<object> _events;
    }
}