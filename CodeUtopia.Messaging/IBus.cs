using System;

namespace CodeUtopia.Messaging
{
    public interface IBus : IUnitOfWork
    {
        void Listen<TCommand>() where TCommand : class;

        void Publish<TEvent>(TEvent message) where TEvent : class;

        void Send<TCommand>(TCommand message) where TCommand : class;

        void Subscribe<TEvent>() where TEvent : class;

        void Defer<TMessage>(TMessage message, TimeSpan delay) where TMessage : class;
    }
}