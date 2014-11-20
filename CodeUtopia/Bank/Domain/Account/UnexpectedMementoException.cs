using System;

namespace CodeUtopia.Bank.Domain.Account
{
    public class UnexpectedMementoException : Exception
    {
        public UnexpectedMementoException(IMemento memento)
            : base(string.Format("The memento \"{0}\" was not unexpected.", memento.GetType()))
        {
        }
    }
}