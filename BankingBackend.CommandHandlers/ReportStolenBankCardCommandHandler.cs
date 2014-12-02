using BankingBackend.Commands.v2;
using BankingBackend.Domain.Client;
using CodeUtopia.Domain;
using NServiceBus;

namespace BankingBackend.CommandHandlers
{
    public class ReportStolenBankCardCommandHandler : IHandleMessages<Commands.v1.ReportStolenBankCardCommand>, IHandleMessages<ReportStolenBankCardCommand>
    {
        public ReportStolenBankCardCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public void Handle(Commands.v1.ReportStolenBankCardCommand reportStolenBankCardCommend)
        {
            var client = _aggregateRepository.Get<Client>(reportStolenBankCardCommend.ClientId);

            var bankCard = client.GetBankCard(reportStolenBankCardCommend.BankCardId);

            bankCard.ReportStolen();

            _aggregateRepository.Commit();
        }

        public void Handle(ReportStolenBankCardCommand reportStolenBankCardCommend)
        {
            var client = _aggregateRepository.Get<Client>(reportStolenBankCardCommend.ClientId);

            var bankCard = client.GetBankCard(reportStolenBankCardCommend.BankCardId);

            bankCard.ReportStolen(reportStolenBankCardCommend.StolenAt);

            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}