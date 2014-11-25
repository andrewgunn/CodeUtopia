using System;

namespace CodeUtopia.Domain
{
    public interface IOriginator
    {
        IMemento CreateMemento();

        void LoadFromMemento(Guid aggregateId, int versionNumber, IMemento memento);
    }
}