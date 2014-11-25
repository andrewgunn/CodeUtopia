namespace CodeUtopia.Messaging
{
    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command) where TCommand : class;
    }
}