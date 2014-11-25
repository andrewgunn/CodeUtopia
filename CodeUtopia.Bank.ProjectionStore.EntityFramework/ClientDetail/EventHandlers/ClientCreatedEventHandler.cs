using CodeUtopia.Bank.Events.v1.Client;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.ClientDetail.EventHandlers
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