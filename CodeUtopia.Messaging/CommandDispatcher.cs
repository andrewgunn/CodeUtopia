namespace CodeUtopia.Messaging
{
    public sealed class CommandDispatcher : ICommandDispatcher
    {
        public CommandDispatcher(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public void Dispatch<TCommand>(TCommand command) where TCommand : class
        {
            var commandHandler = _dependencyResolver.Resolve<ICommandHandler<TCommand>>();

            commandHandler.Handle(command);
        }

        private readonly IDependencyResolver _dependencyResolver;
    }
}