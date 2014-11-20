using System;
using CodeUtopia.Bank.Events.v1.Client;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.Domain.Client
{
    public class BankCard : Entity, IBankCard
    {
        public BankCard(Guid aggregateId, IVersionNumberProvider versionNumberProvider)
            : base(aggregateId, versionNumberProvider)
        {
            RegisterEventHandlers();
        }

        private BankCard(Guid aggregateId, IVersionNumberProvider versionNumberProvider, Guid bankCardId, Guid accountId)
            : this(aggregateId, versionNumberProvider)
        {
            EntityId = bankCardId;
            _accountId = accountId;
        }

        public static BankCard Create(Guid aggregateId,
                                      IVersionNumberProvider versionNumberProvider,
                                      Guid bankCardId,
                                      Guid accountId)
        {
            return new BankCard(aggregateId, versionNumberProvider, bankCardId, accountId);
        }

        private void EnsureNotReportedStolen()
        {
            if (_isReportedStolen)
            {
                throw new BankCardIsReportedStolenException(EntityId);
            }
        }

        private void OnBankCardReportedStolen(BankCardReportedStolen bankCardReportedStolen)
        {
            _isReportedStolen = true;
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<BankCardReportedStolen>(OnBankCardReportedStolen);
        }

        public void ReportStolen()
        {
            EnsureNotReportedStolen();

            Apply(new BankCardReportedStolen(AggregateId, VersionNumberProvider, EntityId));
        }

        private readonly Guid _accountId;

        private bool _isReportedStolen;
    }
}