using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BankReporting.ProjectionStore.ClientDetail
{
    public class ClientDetailEntity
    {
        public ClientDetailEntity()
        {
            Accounts = new Collection<AccountEntity>();
            BankCards = new Collection<BankCardEntity>();
        }

        public virtual ICollection<AccountEntity> Accounts { get; set; }

        public virtual ICollection<BankCardEntity> BankCards { get; set; }

        public Guid ClientId { get; set; }

        public string ClientName { get; set; }
    }
}