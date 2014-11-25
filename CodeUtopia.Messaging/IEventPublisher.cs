namespace CodeUtopia.Messaging
{
    public interface IEventPublisher
    {
        void Dispatch<TEvent>(TEvent message) where TEvent : class;
    }
}