using CodeUtopia.Bank.Events.v1.Account;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.AccountDetail.EventHandlers
{
    public class AmountDepositedEventHandler : IEventHandler<AmountDepositedEvent>
    {
        public AmountDepositedEventHandler(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public void Handle(AmountDepositedEvent amountDepositedEvent)
        {
            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                var accountDetail = databaseContext.AccountDetails.Find(amountDepositedEvent.AccountId);
                accountDetail.Balance = amountDepositedEvent.Balance;

                databaseContext.SaveChanges();
            }
        }

        private readonly string _nameOrConnectionString;
    }
}