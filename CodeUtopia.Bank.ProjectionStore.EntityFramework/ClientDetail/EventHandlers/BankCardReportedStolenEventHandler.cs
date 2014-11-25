using CodeUtopia.Bank.Events.v1.Client;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.ClientDetail.EventHandlers
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
                    return;
                }

                bankCard.IsStolen = true;

                databaseContext.SaveChanges();
            }
        }

        private readonly string _nameOrConnectionString;
    }
}