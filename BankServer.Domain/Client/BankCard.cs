using System;
using CodeUtopia.Domain;

namespace BankServer.Domain.Client
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
            if (_isStolen)
            {
                throw new BankCardAlreadyReportedStolenException(EntityId);
            }
        }

        private void OnBankCardReportedStolen(Events.v1.Client.BankCardReportedStolenEvent bankCardReportedStolenEvent)
        {
            _isStolen = true;
        }

        private void OnBankCardReportedStolen(Events.v2.Client.BankCardReportedStolenEvent bankCardReportedStolenEvent)
        {
            _isStolen = true;
            _stolenAt = bankCardReportedStolenEvent.StolenAt;
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<Events.v1.Client.BankCardReportedStolenEvent>(OnBankCardReportedStolen);
            RegisterEventHandler<Events.v2.Client.BankCardReportedStolenEvent>(OnBankCardReportedStolen);
        }

        [Obsolete]
        public void ReportStolen()
        {
            EnsureIsInitialized();
            EnsureNotReportedStolen();

            Apply(new Events.v1.Client.BankCardReportedStolenEvent(AggregateId, GetNextVersionNumber(), EntityId));
        }

        public void ReportStolen(DateTime stolenAt)
        {
            EnsureIsInitialized();
            EnsureNotReportedStolen();

            Apply(new Events.v2.Client.BankCardReportedStolenEvent(AggregateId, GetNextVersionNumber(), EntityId, stolenAt));
        }

        private readonly Guid _accountId;

        private bool _isStolen;

        private DateTime? _stolenAt;
    }
}