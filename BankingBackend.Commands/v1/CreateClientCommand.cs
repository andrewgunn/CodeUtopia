using System;
using Newtonsoft.Json;

namespace BankingBackend.Commands.v1
{
    public class CreateClientCommand
    {
        [JsonConstructor]
        public CreateClientCommand(Guid clientId, string clientName)
        {
            ClientId = clientId;
            ClientName = clientName;
        }

        public Guid ClientId { get; set; }

        public string ClientName { get; set; }
    }
}