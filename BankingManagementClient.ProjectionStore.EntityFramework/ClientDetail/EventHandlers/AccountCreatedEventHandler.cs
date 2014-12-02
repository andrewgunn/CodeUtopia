using BankingBackend.Events.v1.Account;
using CodeUtopia;
using NServiceBus;

namespace BankingManagementClient.ProjectionStore.EntityFramework.ClientDetail.EventHandlers
{
    public class AccountCreatedEventHandler : IHandleMessages<AccountCreatedEvent>
    {
        public AccountCreatedEventHandler()
        {
            _nameOrConnectionString = "ProjectionStore";
        }

        public void Handle(AccountCreatedEvent accountCreatedEvent)
        {
            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                var account = new AccountEntity
                              {
                                  AccountId = accountCreatedEvent.AccountId,
                                  ClientId = accountCreatedEvent.ClientId,
                                  AccountName = accountCreatedEvent.AccountName
                              };

                databaseContext.Accounts.Add(account);

                databaseContext.SaveChanges();
            }
        }

        private readonly string _nameOrConnectionString;
    }
}