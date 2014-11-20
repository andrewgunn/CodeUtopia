using System.Data.Entity.ModelConfiguration;

namespace CodeUtopia.EventStore.EntityFramework
{
    internal class SnapshotConfiguration : EntityTypeConfiguration<Snapshot>
    {
        public SnapshotConfiguration()
        {
            ToTable("Snapshot", "EventStore");

            var columnOrder = 0;

            Property(p => p.Id)
                .HasColumnName("SnapshotId")
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