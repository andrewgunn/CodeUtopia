namespace CodeUtopia.Messaging
{
    public interface ICommandSender
    {
        void Dispatch<TCommand>(TCommand command) where TCommand : class;
    }
}