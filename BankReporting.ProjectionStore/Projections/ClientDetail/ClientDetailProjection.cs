using System;
using System.Collections.Generic;

namespace BankReporting.ProjectionStore.Projections.ClientDetail
{
    public class ClientDetailProjection
    {
        public ClientDetailProjection(Guid clientId,
                                      string clientName,
                                      IReadOnlyList<AccountProjection> accounts,
                                      IReadOnlyCollection<BankCardProjection> bankCards)
        {
            _clientId = clientId;
            _clientName = clientName;
            _accounts = accounts;
            _bankCards = bankCards;
        }

        public IReadOnlyCollection<AccountProjection> Accounts
        {
            get
            {
                return _accounts;
            }
        }

        public IReadOnlyCollection<BankCardProjection> BankCards
        {
            get
            {
                return _bankCards;
            }
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

        private readonly IReadOnlyList<AccountProjection> _accounts;

        private readonly IReadOnlyCollection<BankCardProjection> _bankCards;

        private readonly Guid _clientId;

        private readonly string _clientName;
    }
}