using BankServer.Events.v1.Client;
using CodeUtopia;

namespace BankReporting.ProjectionStore.ClientDetail.EventHandlers
{
    public class AccountAssignedEventHandler : IEventHandler<AccountAssignedEvent>
    {
        public AccountAssignedEventHandler(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public void Handle(AccountAssignedEvent accountAssignedEvent)
        {
            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                var account = new AccountEntity
                              {
                                  AccountId = accountAssignedEvent.AccountId,
                                  ClientId = accountAssignedEvent.ClientId,
                                  AccountName = accountAssignedEvent.AccountName
                              };

                databaseContext.Accounts.Add(account);

                databaseContext.SaveChanges();
            }
        }

        private readonly string _nameOrConnectionString;
    }
}