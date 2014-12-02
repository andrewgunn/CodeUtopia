using BankingBackend.Events.v1.Account;
using CodeUtopia;
using NServiceBus;

namespace BankingManagementClient.ProjectionStore.EntityFramework.AccountDetail.EventHandlers
{
    public class AmountWithdrawnEventHandler : IHandleMessages<AmountWithdrawnEvent>
    {
        public AmountWithdrawnEventHandler()
        {
            _nameOrConnectionString = "ProjectionStore";
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