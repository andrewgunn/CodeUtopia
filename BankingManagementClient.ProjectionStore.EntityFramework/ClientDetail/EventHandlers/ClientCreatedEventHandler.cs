using BankingBackend.Events.v1.Client;
using CodeUtopia;
using NServiceBus;

namespace BankingManagementClient.ProjectionStore.EntityFramework.ClientDetail.EventHandlers
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
                var client = new ClientDetailEntity
                             {
                                 ClientId = clientCreatedEvent.ClientId,
                                 ClientName = clientCreatedEvent.ClientName
                             };

                databaseContext.ClientDetails.Add(client);

                databaseContext.SaveChanges();
            }
        }

        private readonly string _nameOrConnectionString;
    }
}