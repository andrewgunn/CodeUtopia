namespace CodeUtopia.Messaging
{
    public interface IBus : IUnitOfWork
    {
        void Publish<T>(T message) where T : class;

        void Send<T>(T message) where T : class;
    }
}