using BankServer.Commands.v1;
using BankServer.Domain.Client;
using CodeUtopia;
using CodeUtopia.Domain;

namespace BankServer.CommandHandlers
{
    public class ReportStolenBankCardCommandHandler : ICommandHandler<ReportStolenBankCardCommand>
        //,ICommandHandler<Commands.v2.ReportStolenBankCardCommand>
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

        //public void Handle(Commands.v2.ReportStolenBankCardCommand reportStolenBankCardCommend)
        //{
        //    var client = _aggregateRepository.Get<Client>(reportStolenBankCardCommend.ClientId);

        //    var bankCard = client.GetBankCard(reportStolenBankCardCommend.BankCardId);

        //    bankCard.ReportStolen(reportStolenBankCardCommend.StolenAt);

        //    _aggregateRepository.Commit();
        //}

        private readonly IAggregateRepository _aggregateRepository;
    }
}