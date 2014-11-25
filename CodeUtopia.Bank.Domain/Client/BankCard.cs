using System;
using CodeUtopia.Bank.Events.v1.Client;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.Domain.Client
{
    public class BankCard : Entity, IBankCard
    {
        private BankCard(Guid aggregateId, IVersionNumberProvider versionNumberProvider, Guid bankCardId, Guid accountId)
            : base(aggregateId, versionNumberProvider)
        {
            EntityId = bankCardId;
            _accountId = accountId;

            RegisterEventHandlers();
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

        private void OnBankCardReportedStolen(BankCardReportedStolenEvent bankCardReportedStolenEvent)
        {
            _isReportedStolen = true;
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<BankCardReportedStolenEvent>(OnBankCardReportedStolen);
        }

        public void ReportStolen()
        {
            EnsureNotReportedStolen();

            Apply(new BankCardReportedStolenEvent(AggregateId, GetNextVersionNumber(), EntityId));
        }

        private readonly Guid _accountId;

        private bool _isReportedStolen;
    }
}