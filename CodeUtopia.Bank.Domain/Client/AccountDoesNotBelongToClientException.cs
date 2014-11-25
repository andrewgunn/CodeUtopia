using System;

namespace CodeUtopia.Bank.Domain.Client
{
    public class AccountDoesNotBelongToClientException : Exception
    {
        public AccountDoesNotBelongToClientException(Guid clientId, Guid accountId)
            : base(string.Format("The account {1} does not belong to the client {0}.", clientId, accountId))
        {
        }
    }
}