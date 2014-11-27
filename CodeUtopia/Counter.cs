using System.Threading;

namespace CodeUtopia
{
    public class Counter
    {
        private int _count = 0;

        public int Increment()
        {
            return Interlocked.Increment(ref _count);
        }

        public void Reset()
        {
            _count = 0;
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

    }
}