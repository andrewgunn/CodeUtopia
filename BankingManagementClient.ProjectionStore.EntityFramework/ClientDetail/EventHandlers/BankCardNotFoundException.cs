using System;

namespace BankingManagementClient.ProjectionStore.EntityFramework.ClientDetail.EventHandlers
{
    public class BankCardNotFoundException : Exception
    {
        public BankCardNotFoundException(Guid bankCardId)
            : base(string.Format("The bank card {0} cannot be found.", bankCardId))
        {
        }
    }
}