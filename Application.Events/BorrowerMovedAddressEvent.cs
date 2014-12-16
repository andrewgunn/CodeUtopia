using System;
using CodeUtopia.Events;

namespace Application.Events
{
    public class BorrowerMovedAddressEvent : IEntityEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public string County { get; set; }

        public string District { get; set; }

        public Guid EntityId { get; set; }

        public string Flat { get; set; }

        public string HouseName { get; set; }

        public string HouseNumber { get; set; }

        public string Postcode { get; set; }

        public string Street { get; set; }

        public string Street2 { get; set; }

        public string TownOrCity { get; set; }
    }
}