namespace CodeUtopia.Messaging
{
    public interface IEventCoordinator
    {
        void Coordinate<TEvent>(TEvent @event, IBus bus) where TEvent : class;
    }
}