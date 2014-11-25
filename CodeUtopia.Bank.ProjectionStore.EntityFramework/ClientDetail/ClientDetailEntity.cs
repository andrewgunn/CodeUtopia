using System;
using System.Collections.Generic;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.ClientDetail
{
    public class ClientDetailEntity
    {
        public virtual ICollection<AccountEntity> Accounts { get; set; }

        public virtual ICollection<BankCardEntity> BankCards { get; set; }

        public Guid ClientId { get; set; }

        public string ClientName { get; set; }
    }
}