using System;

namespace CodeUtopia.Domain
{
    public class AggregateNotInitializedException : Exception
    {
        public AggregateNotInitializedException()
            : base("The aggregate has not been initialised.")
        {
        }
    }
}