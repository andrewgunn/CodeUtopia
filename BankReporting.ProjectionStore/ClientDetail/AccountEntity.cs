using System;

namespace BankReporting.ProjectionStore.ClientDetail
{
    public class AccountEntity
    {
        public Guid AccountId { get; set; }

        public string AccountName { get; set; }

        public Guid ClientId { get; set; }
    }
}