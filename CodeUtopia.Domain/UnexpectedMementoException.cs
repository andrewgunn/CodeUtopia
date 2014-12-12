using System;

namespace CodeUtopia.Domain
{
    public class UnexpectedMementoException : Exception
    {
        public UnexpectedMementoException(object memento)
            : base(string.Format("The memento \"{0}\" was not unexpected.", memento.GetType()))
        {
        }
    }
}