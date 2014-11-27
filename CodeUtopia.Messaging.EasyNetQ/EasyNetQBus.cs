using System;
using System.Collections.Concurrent;
using System.Threading;

namespace CodeUtopia.Messaging.EasyNetQ
{
    public class EasyNetQBus : IBus
    {
        public EasyNetQBus(global::EasyNetQ.IBus bus,
                           ICommandHandlerResolver commandHandlerResolver,
                           ICommandSender commandDispatcher,
                           IEventHandlerResolver eventHandlerResolver,
                           IEventPublisher eventDispatcher)
        {
            _bus = bus;
            _commandHandlerResolver = commandHandlerResolver;
            _commandDispatcher = commandDispatcher;
            _eventHandlerResolver = eventHandlerResolver;
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

            const string endpointName = "BankingManagementClient";
            var subscriptionId = string.Format("{0}-{1}Subscription", endpointName, eventType.Name);

            _bus.Send(subscriptionId, @event);
        }

        public void Listen<TCommand>() where TCommand : class
        {
            const string queue = "BankingBackend";

            _bus.Receive(queue,
                         registration => registration.Add((TCommand x) =>
                                                          {
                                                              var commandHandler =
                                                                  _commandHandlerResolver.Resolve<TCommand>();

                                                              commandHandler.Handle(x);
                                                          }));
        }

        public void Publish<TEvent>(TEvent message) where TEvent : class
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

        public void Send<TCommand>(TCommand message) where TCommand : class
        {
            // TODO Make this asynchronous.
            _commands.Enqueue(message);
        }

        private void SendCore(object command)
        {
            // TODO Make this asynchronous.
            _commandDispatcher.Send((dynamic)command);
        }

        public void Subscribe<TEvent>() where TEvent : class
        {
            var eventType = typeof(TEvent);

            const string endpointName = "BankingManagementClient";
            var subscriptionId = string.Format("{0}-{1}Subscription", endpointName, eventType.Name);

            _bus.Subscribe(subscriptionId,
                           (TEvent x) =>
                           {
                               var eventHandlers = _eventHandlerResolver.Resolve<TEvent>();

                               foreach (var eventHandler in eventHandlers)
                               {
                                   eventHandler.Handle(x);
                               }
                           });
        }

        private readonly global::EasyNetQ.IBus _bus;

        private readonly ICommandSender _commandDispatcher;

        private readonly ICommandHandlerResolver _commandHandlerResolver;

        private ConcurrentQueue<object> _commands;

        private readonly IEventPublisher _eventDispatcher;

        private readonly IEventHandlerResolver _eventHandlerResolver;

        private ConcurrentQueue<object> _events;
    }
}