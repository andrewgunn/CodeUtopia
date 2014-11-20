using System.Data.Entity.ModelConfiguration;

namespace CodeUtopia.EventStore.EntityFramework
{
    internal class DomainEventConfiguration : EntityTypeConfiguration<DomainEvent>
    {
        public DomainEventConfiguration()
        {
            ToTable("DomainEvent", "EventStore");

            var columnOrder = 0;

            Property(p => p.Id)
                .HasColumnName("DomainEventId")
                .HasColumnOrder(++columnOrder);
            Property(p => p.AggregateId)
                .HasColumnOrder(++columnOrder);
            Property(p => p.VersionNumber)
                .HasColumnOrder(++columnOrder);
            Property(p => p.Data)
                .HasColumnOrder(++columnOrder);
        }
    }
}