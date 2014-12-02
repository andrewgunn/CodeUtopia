using BankingBackend.Events.v2.Client;
using CodeUtopia;
using NServiceBus;

namespace BankingManagementClient.ProjectionStore.EntityFramework.ClientDetail.EventHandlers
{
    public class BankCardReportedStolenEventHandler : IHandleMessages<BankCardReportedStolenEvent>
    {
        public BankCardReportedStolenEventHandler()
        {
            _nameOrConnectionString = "ProjectionStore";
        }

        public void Handle(BankCardReportedStolenEvent bankCardReportedStolenEvent)
        {
            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                var bankCard = databaseContext.BankCards.Find(bankCardReportedStolenEvent.BankCardId);

                if (bankCard == null)
                {
                    return;
                }

                bankCard.IsStolen = true;
                bankCard.StolenAt = bankCardReportedStolenEvent.StolenAt;

                databaseContext.SaveChanges();
            }
        }

        private readonly string _nameOrConnectionString;
    }
}