using System;

namespace BankReporting.ProjectionStore.EntityFramework.ClientDetail
{
    public class AccountEntity
    {
        public Guid AccountId { get; set; }

        public string AccountName { get; set; }

        public Guid ClientId { get; set; }
    }
}