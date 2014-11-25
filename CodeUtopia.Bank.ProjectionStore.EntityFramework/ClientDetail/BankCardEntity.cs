using System;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.ClientDetail
{
    public class BankCardEntity
    {
        public virtual AccountEntity Account { get; set; }

        public Guid AccountId { get; set; }

        public Guid Id { get; set; }
    }
}