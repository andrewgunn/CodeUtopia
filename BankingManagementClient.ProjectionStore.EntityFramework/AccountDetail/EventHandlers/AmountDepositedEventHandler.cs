using BankingBackend.Events.v1.Account;
using CodeUtopia;
using NServiceBus;

namespace BankingManagementClient.ProjectionStore.EntityFramework.AccountDetail.EventHandlers
{
    public class AmountDepositedEventHandler : IHandleMessages<AmountDepositedEvent>
    {
        public AmountDepositedEventHandler()
        {
            _nameOrConnectionString = "ProjectionStore";
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