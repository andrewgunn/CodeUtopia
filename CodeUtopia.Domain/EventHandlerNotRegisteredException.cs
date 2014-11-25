using System;

namespace CodeUtopia.Domain
{
    public class EventHandlerNotRegisteredException : Exception
    {
        public EventHandlerNotRegisteredException(Type eventType)
            : base(string.Format("The event handler for '{0}' has not registered.", eventType.FullName))
        {
        }
    }
}