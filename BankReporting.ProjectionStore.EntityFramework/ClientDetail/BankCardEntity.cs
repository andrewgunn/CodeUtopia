using System;

namespace BankReporting.ProjectionStore.EntityFramework.ClientDetail
{
    public class BankCardEntity
    {
        public Guid AccountId { get; set; }

        public Guid BankCardId { get; set; }

        public Guid ClientId { get; set; }

        public bool IsStolen { get; set; }

        public DateTime? StolenAt { get; set; }
    }
}