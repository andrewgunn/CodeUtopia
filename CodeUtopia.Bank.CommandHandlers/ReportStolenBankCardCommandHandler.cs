using CodeUtopia.Bank.Commands.v1;
using CodeUtopia.Bank.Domain.Client;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.CommandHandlers
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