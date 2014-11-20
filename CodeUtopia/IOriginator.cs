using System;

namespace CodeUtopia
{
    public interface IOriginator
    {
        IMemento CreateMemento();

        void LoadFromMemento(Guid aggregateId, int versionNumber, IMemento memento);
    }
}