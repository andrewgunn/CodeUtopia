namespace CodeUtopia.Messaging
{
    public interface IEventHandlerResolver
    {
        IEventHandler<TEvent>[] Resolve<TEvent>() where TEvent : class;
    }
}