using System;

namespace CodeUtopia.Bank.Commands.v1
{
    public class CreateClientCommand
    {
        public CreateClientCommand(Guid clientId, string clientName)
        {
            _clientId = clientId;
            _clientName = clientName;
        }

        public Guid ClientId
        {
            get
            {
                return _clientId;
            }
        }

        public string ClientName
        {
            get
            {
                return _clientName;
            }
        }

        private readonly Guid _clientId;

        private readonly string _clientName;
    }
}