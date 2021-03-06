using System.Data.Entity.ModelConfiguration;

namespace CodeUtopia.WriteStore.EntityFramework
{
    internal class DomainEventEntityConfiguration : EntityTypeConfiguration<DomainEventEntity>
    {
        public DomainEventEntityConfiguration()
        {
            ToTable("DomainEvent", "WriteStore");

            HasKey(x => new
                        {
                            x.AggregateId,
                            x.AggregateVersionNumber
                        });

            var columnOrder = 0;

            Property(p => p.AggregateId)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.AggregateVersionNumber)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.DomainEventType)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.DomainEvent)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
        }
    }
}