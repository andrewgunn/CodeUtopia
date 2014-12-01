using BankServer.Events.v1.Client;
using CodeUtopia;

namespace BankReporting.ProjectionStore.EntityFramework.Client.EventHandlers
{
    public class ClientCreatedEventHandler : IEventHandler<ClientCreatedEvent>
    {
        public ClientCreatedEventHandler(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
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