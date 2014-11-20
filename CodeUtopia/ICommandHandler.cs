namespace CodeUtopia
{
    public interface ICommandHandler
    {
        void Execute(ICommand command);
    }
}