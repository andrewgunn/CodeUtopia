using System.Data.Entity.ModelConfiguration;

namespace CodeUtopia.WriteStore.EntityFramework
{
    internal class SnapshotEntityConfiguration : EntityTypeConfiguration<SnapshotEntity>
    {
        public SnapshotEntityConfiguration()
        {
            ToTable("Snapshot", "WriteStore");

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
            Property(p => p.MementoType)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.Memento)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
        }
    }
}