using System;

namespace BankingClient.Host.Console
{
    public sealed class RandomSingleton
    {
        private static readonly Random _random;

        static  RandomSingleton()
        {
            _random = new Random();
        }

        public static Random Instance
        {
            get { return _random; }
        }
    }
}