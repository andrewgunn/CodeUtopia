using BankServer.Events.v2.Client;
using CodeUtopia;

namespace BankReporting.ProjectionStore.EntityFramework.ClientDetail.EventHandlers
{
    public class BankCardReportedStolenEventHandler : IEventHandler<BankCardReportedStolenEvent>
    {
        public BankCardReportedStolenEventHandler(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public void Handle(BankCardReportedStolenEvent bankCardReportedStolenEvent)
        {
            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                var bankCard = databaseContext.BankCards.Find(bankCardReportedStolenEvent.BankCardId);

                if (bankCard == null)
                {
                    throw new BankCardNotFoundException(bankCardReportedStolenEvent.BankCardId);
                }

                bankCard.IsStolen = true;
                bankCard.StolenAt = bankCardReportedStolenEvent.StolenAt;

                databaseContext.SaveChanges();
            }
        }

        private readonly string _nameOrConnectionString;
    }
}