using System;

namespace BankingManagementClient.ProjectionStore.Projections.ClientDetail
{
    public class BankCardProjection
    {
        public BankCardProjection(Guid bankCardId, Guid accountId)
        {
            _bankCardId = bankCardId;
            _accountId = accountId;
        }

        public Guid AccountId
        {
            get
            {
                return _accountId;
            }
        }

        public Guid BankCardId
        {
            get
            {
                return _bankCardId;
            }
        }

        private readonly Guid _accountId;

        private readonly Guid _bankCardId;
    }
}