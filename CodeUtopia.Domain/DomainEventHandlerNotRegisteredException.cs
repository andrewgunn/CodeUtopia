using System;

namespace CodeUtopia.Domain
{
    public class DomainEventHandlerNotRegisteredException : Exception
    {
        public DomainEventHandlerNotRegisteredException(Type eventType)
            : base(string.Format("The domain event handler for '{0}' has not registered.", eventType.FullName))
        {
        }
    }
}