using BankingBackend.Events.v1.Client;
using CodeUtopia;
using NServiceBus;

namespace BankingManagementClient.ProjectionStore.EntityFramework.ClientDetail.EventHandlers
{
    public class NewBankCardAssignedEventHandler : IHandleMessages<NewBankCardAssignedEvent>
    {
        public NewBankCardAssignedEventHandler()
        {
            _nameOrConnectionString = "ProjectionStore";
        }

        public void Handle(NewBankCardAssignedEvent newBankCardAssignedEvent)
        {
            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                databaseContext.BankCards.Add(new BankCardEntity
                                              {
                                                  BankCardId = newBankCardAssignedEvent.BankCardId,
                                                  ClientId = newBankCardAssignedEvent.ClientId,
                                                  AccountId = newBankCardAssignedEvent.AccountId
                                              });

                databaseContext.SaveChanges();
            }
        }

        private readonly string _nameOrConnectionString;
    }
}