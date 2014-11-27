using BankingBackend.Events.v1.Account;
using CodeUtopia;

namespace BankingManagementClient.ProjectionStore.EntityFramework.AccountDetail.EventHandlers
{
    public class AmountWithdrawnEventHandler : IEventHandler<AmountWithdrawnEvent>
    {
        public AmountWithdrawnEventHandler(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public void Handle(AmountWithdrawnEvent amountWithdrawnEvent)
        {
            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                var accountDetail = databaseContext.AccountDetails.Find(amountWithdrawnEvent.AccountId);
                accountDetail.Balance = amountWithdrawnEvent.Balance;

                databaseContext.SaveChanges();
            }
        }

        private readonly string _nameOrConnectionString;
    }
}