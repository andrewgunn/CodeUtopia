namespace BankingBackend.Domain.Client
{
    public class ClientName
    {
        public ClientName(string clientName)
        {
            _clientName = clientName;
        }

        public static implicit operator string(ClientName clientName)
        {
            return clientName._clientName;
        }

        public static implicit operator ClientName(string clientName)
        {
            return new ClientName(clientName);
        }

        private readonly string _clientName;
    }
}