namespace CodeUtopia.Messaging
{
    public sealed class InProcCommandSender : ICommandSender
    {
        public InProcCommandSender(ICommandHandlerResolver commandHandlerResolver)
        {
            _commandHandlerResolver = commandHandlerResolver;
        }

        public void Send<TCommand>(TCommand command) where TCommand : class
        {
            var commandHandler = _commandHandlerResolver.Resolve<TCommand>();

            commandHandler.Handle(command);
        }

        private readonly ICommandHandlerResolver _commandHandlerResolver;
    }
}