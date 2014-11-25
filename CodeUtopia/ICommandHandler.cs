namespace CodeUtopia
{
    public interface ICommandHandler<in TCommand>
        where TCommand : class
    {
        void Handle(TCommand command);
    }
}