using BankServer.Events.v1.Account;
using CodeUtopia;

namespace BankReporting.ProjectionStore.EntityFramework.AccountDetail.EventHandlers
{
    public class AccountCreatedEventHandler : IEventHandler<AccountCreatedEvent>
    {
        public AccountCreatedEventHandler(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public void Handle(AccountCreatedEvent accountCreatedEvent)
        {
            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                var accountDetail = new AccountDetailEntity
                                    {
                                        AccountId = accountCreatedEvent.AccountId,
                                        ClientId = accountCreatedEvent.ClientId,
                                        AccountName = accountCreatedEvent.AccountName
                                    };

                databaseContext.AccountDetails.Add(accountDetail);

                databaseContext.SaveChanges();
            }
        }

        private readonly string _nameOrConnectionString;
    }
}