using System;

namespace BankServer.Domain.Client
{
    public class BankCardNotFoundException : Exception
    {
        public BankCardNotFoundException(Guid bankCardId)
            : base(string.Format("The bank card {0} cannot be found.", bankCardId))
        {
        }
    }
}