using System;

namespace CodeUtopia
{
    public class UtcNowDateTimeProvider : IDateTimeProvider
    {
        public UtcNowDateTimeProvider()
        {
            _utcNow = DateTime.UtcNow;
        }

        public DateTime Value
        {
            get
            {
                return _utcNow;
            }
        }

        private readonly DateTime _utcNow;
    }
}