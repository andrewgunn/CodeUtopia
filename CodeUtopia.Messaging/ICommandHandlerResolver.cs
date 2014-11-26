namespace CodeUtopia.Messaging
{
    public interface ICommandHandlerResolver
    {
        ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : class;
    }

}