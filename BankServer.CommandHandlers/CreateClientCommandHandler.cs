using BankServer.Commands.v1;
using BankServer.Domain.Client;
using CodeUtopia;
using CodeUtopia.Domain;

namespace BankServer.CommandHandlers
{
    public class CreateClientCommandHandler : ICommandHandler<CreateClientCommand>
    {
        public CreateClientCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public void Handle(CreateClientCommand createClientCommand)
        {
            var client = Client.Create(createClientCommand.ClientId, createClientCommand.ClientName);

            _aggregateRepository.Add(client);
            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}