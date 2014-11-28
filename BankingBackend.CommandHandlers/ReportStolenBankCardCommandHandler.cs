using System;
using BankingBackend.Commands.v1;
using BankingBackend.Domain.Client;
using CodeUtopia;
using CodeUtopia.Domain;

namespace BankingBackend.CommandHandlers
{
    public class ReportStolenBankCardCommandHandler : ICommandHandler<ReportStolenBankCardCommand>
    {
        public ReportStolenBankCardCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public void Handle(ReportStolenBankCardCommand reportStolenBankCardCommend)
        {
            var client = _aggregateRepository.Get<Client>(reportStolenBankCardCommend.ClientId);

            var bankCard = client.GetBankCard(reportStolenBankCardCommend.BankCardId);

            bankCard.ReportStolen();

            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}