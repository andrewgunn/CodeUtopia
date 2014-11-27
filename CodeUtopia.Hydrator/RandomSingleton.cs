using System;

namespace CodeUtopia.Hydrator
{
    public sealed class RandomSingleton
    {
        static RandomSingleton()
        {
            _random = new Random();
        }

        public static Random Instance
        {
            get
            {
                return _random;
            }
        }

        private static readonly Random _random;
    }
}