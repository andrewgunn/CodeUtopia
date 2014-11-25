namespace CodeUtopia.Messaging
{
    public interface ICommandSender
    {
        void Send<TCommand>(TCommand command) where TCommand : class;
    }
}