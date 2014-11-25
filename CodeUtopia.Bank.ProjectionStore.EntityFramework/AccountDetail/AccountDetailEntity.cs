using System;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.AccountDetail
{
    public class AccountDetailEntity
    {
        public string AccountName { get; set; }

        public decimal Balance { get; set; }

        public Guid Id { get; set; }
    }
}