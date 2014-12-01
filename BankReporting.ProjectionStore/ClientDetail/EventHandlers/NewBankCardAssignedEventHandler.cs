using BankServer.Events.v1.Client;
using CodeUtopia;

namespace BankReporting.ProjectionStore.ClientDetail.EventHandlers
{
    public class NewBankCardAssignedEventHandler : IEventHandler<NewBankCardAssignedEvent>
    {
        public NewBankCardAssignedEventHandler(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
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