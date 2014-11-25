namespace CodeUtopia.Messaging
{
    public interface IEventDispatcher
    {
        void Dispatch<TEvent>(TEvent message) where TEvent : class;
    }
}