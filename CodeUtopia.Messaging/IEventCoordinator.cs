namespace CodeUtopia.Messaging
{
    public interface IEventCoordinator
    {
        void Coordinate<TEvent>(TEvent @event) where TEvent : class;
    }
}