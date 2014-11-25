namespace CodeUtopia.Messaging
{
    public sealed class CommandSender : ICommandSender
    {
        public CommandSender(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public void Send<TCommand>(TCommand command) where TCommand : class
        {
            var commandHandler = _dependencyResolver.Resolve<ICommandHandler<TCommand>>();

            commandHandler.Handle(command);
        }

        private readonly IDependencyResolver _dependencyResolver;
    }
}