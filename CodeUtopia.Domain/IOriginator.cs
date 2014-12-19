using System;

namespace CodeUtopia.Domain
{
    public interface IOriginator
    {
        object CreateMemento();

        void LoadFromMemento(Guid aggregateId, int aggregateVersionNumber, object memento);
    }
}