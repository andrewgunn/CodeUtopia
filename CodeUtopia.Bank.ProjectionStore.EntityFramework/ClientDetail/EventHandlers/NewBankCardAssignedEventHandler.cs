using CodeUtopia.Bank.Events.v1.Client;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.ClientDetail.EventHandlers
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
                var clientDetail = databaseContext.ClientDetails.Find(newBankCardAssignedEvent.ClientId);
                clientDetail.BankCards.Add(new BankCardEntity
                                           {
                                               Id = newBankCardAssignedEvent.BankCardId,
                                               AccountId = newBankCardAssignedEvent.AccountId
                                           });

                databaseContext.SaveChanges();
            }
        }

        private readonly string _nameOrConnectionString;
    }
}