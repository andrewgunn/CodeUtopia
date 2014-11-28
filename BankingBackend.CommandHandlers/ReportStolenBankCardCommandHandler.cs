using System;
using BankingBackend.Commands;
using BankingBackend.Commands.v2;
using BankingBackend.Domain.Client;
using CodeUtopia;
using CodeUtopia.Domain;

namespace BankingBackend.CommandHandlers
{
    public class ReportStolenBankCardCommandHandler : ICommandHandler<Commands.v1.ReportStolenBankCardCommand>, ICommandHandler<ReportStolenBankCardCommand>
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