namespace CodeUtopia.Messaging
{
    public sealed class CommandHandlerResolver : ICommandHandlerResolver
    {
        public CommandHandlerResolver(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : class
        {
            var commandHandler = _dependencyResolver.Resolve<ICommandHandler<TCommand>>();

            return commandHandler;
        }

        private readonly IDependencyResolver _dependencyResolver;
    }
}