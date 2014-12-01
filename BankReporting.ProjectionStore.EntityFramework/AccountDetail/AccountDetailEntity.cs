using System;

namespace BankReporting.ProjectionStore.EntityFramework.AccountDetail
{
    public class AccountDetailEntity
    {
        public Guid AccountId { get; set; }

        public string AccountName { get; set; }

        public decimal Balance { get; set; }

        public Guid ClientId { get; set; }
    }
}