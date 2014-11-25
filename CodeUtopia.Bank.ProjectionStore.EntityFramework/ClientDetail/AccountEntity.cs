using System;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.ClientDetail
{
    public class AccountEntity
    {
        public string AccountName { get; set; }

        public Guid ClientId { get; set; }

        public Guid Id { get; set; }
    }
}