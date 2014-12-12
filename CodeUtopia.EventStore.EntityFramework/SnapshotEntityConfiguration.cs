using System.Data.Entity.ModelConfiguration;

namespace CodeUtopia.EventStore.EntityFramework
{
    internal class SnapshotEntityConfiguration : EntityTypeConfiguration<SnapshotEntity>
    {
        public SnapshotEntityConfiguration()
        {
            ToTable("Snapshot", "EventStore");

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
            Property(p => p.Data)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
        }
    }
}