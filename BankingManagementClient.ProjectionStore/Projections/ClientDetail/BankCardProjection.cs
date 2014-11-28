using System;

namespace BankingManagementClient.ProjectionStore.Projections.ClientDetail
{
    public class BankCardProjection
    {
        public BankCardProjection(Guid bankCardId, Guid accountId, bool isStolen)
        {
            _bankCardId = bankCardId;
            _accountId = accountId;
            _isStolen = isStolen;
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

        public bool IsStolen
        {
            get
            {
                return _isStolen;
            }
        }

        private readonly Guid _accountId;
        private readonly bool _isStolen;

        private readonly Guid _bankCardId;
    }
}