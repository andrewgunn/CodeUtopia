using System;

namespace CodeUtopia.Messaging
{
    public interface IBus : IUnitOfWork
    {
        void Defer<TMessage>(TMessage message, TimeSpan delay) where TMessage : class;

        void Listen<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6>() where TCommand1 : class
            where TCommand2 : class where TCommand3 : class where TCommand4 : class where TCommand5 : class
            where TCommand6 : class;

        void Publish<TEvent>(TEvent message) where TEvent : class;

        void Send<TCommand>(TCommand message) where TCommand : class;

        void Subscribe<TEvent>() where TEvent : class;
    }
}