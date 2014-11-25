using System;

namespace CodeUtopia
{
    public interface IQueue
    {
        void Pop(Action<object> popAction);

        void Put(object item);
    }
}