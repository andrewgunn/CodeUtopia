using CodeUtopia.Bank.Events.v1.Account;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.AccountDetail.EventHandlers
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
                                        Id = accountCreatedEvent.AccountId,
                                        AccountName = accountCreatedEvent.AccountName
                                    };

                databaseContext.AccountDetails.Add(accountDetail);

                databaseContext.SaveChanges();
            }
        }

        private readonly string _nameOrConnectionString;
    }
}