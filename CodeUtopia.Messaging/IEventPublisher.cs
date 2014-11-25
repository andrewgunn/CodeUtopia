namespace CodeUtopia.Messaging
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent message) where TEvent : class;
    }
}