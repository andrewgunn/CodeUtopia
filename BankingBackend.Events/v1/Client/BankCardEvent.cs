using System;
using CodeUtopia.Events;

namespace BankingBackend.Events.v1.Client
{
    [Serializable]
    public abstract class BankCardEvent : ClientEntityEvent
    {
        protected BankCardEvent(Guid clientId, int versionNumber, Guid bankCardId)
            : base(clientId, versionNumber, bankCardId)
        {
        }

        public Guid BankCardId
        {
            get
            {
                return ((IEntityEvent)this).EntityId;
            }
        }
    }
}