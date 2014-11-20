namespace CodeUtopia
{
    public interface IEventPublisher : IUnitOfWork
    {
        void Publish<TEvent>(TEvent domainEvent) where TEvent : class;
    }
}