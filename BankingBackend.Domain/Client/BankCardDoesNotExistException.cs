﻿using System;

namespace BankingBackend.Domain.Client
{
    public class BankCardDoesNotExistException : Exception
    {
        public BankCardDoesNotExistException(Guid bankCardId)
            : base(string.Format("The bank card {0} does not exist.", bankCardId))
        {
        }
    }
}