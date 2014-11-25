using System;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.Client
{
    public class ClientEntity
    {
        public Guid ClientId { get; set; }

        public string ClientName { get; set; }
    }
}