using BankingBackend.Events.v1.Client;
using CodeUtopia;
using NServiceBus;

namespace BankingManagementClient.ProjectionStore.EntityFramework.Client.EventHandlers
{
    public class ClientCreatedEventHandler : IHandleMessages<ClientCreatedEvent>
    {
        public ClientCreatedEventHandler()
        {
            _nameOrConnectionString = "ProjectionStore";
        }

        public void Handle(ClientCreatedEvent clientCreatedEvent)
        {
            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                var client = new ClientEntity
                             {
                                 ClientId = clientCreatedEvent.ClientId,
                                 ClientName = clientCreatedEvent.ClientName
                             };

                databaseContext.Clients.Add(client);

                databaseContext.SaveChanges();
            }
        }

        private readonly string _nameOrConnectionString;
    }
}