using System;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.AccountDetail
{
    public class AccountDetailEntity
    {
        public Guid AccountId { get; set; }

        public string AccountName { get; set; }

        public decimal Balance { get; set; }
    }
}