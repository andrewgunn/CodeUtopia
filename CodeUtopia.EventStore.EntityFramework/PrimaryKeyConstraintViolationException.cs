using System;

namespace CodeUtopia.EventStore.EntityFramework
{
    public class PrimaryKeyConstraintViolationException : Exception
    {
        public PrimaryKeyConstraintViolationException(params string[] keys)
        {
            _keys = keys;
        }

        public string[] Keys
        {
            get
            {
                return _keys;
            }
        }

        private readonly string[] _keys;
    }
}