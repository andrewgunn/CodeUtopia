using System.Data.Entity.ModelConfiguration;

namespace CodeUtopia.EventStore.EntityFramework
{
    internal class AggregateConfiguration : EntityTypeConfiguration<Aggregate>
    {
        public AggregateConfiguration()
        {
            ToTable("Aggregate", "EventStore");

            var columnOrder = 0;

            Property(p => p.Id)
                .HasColumnName("AggregateId")
                .HasColumnOrder(++columnOrder);
            Property(p => p.VersionNumber)
                .HasColumnOrder(++columnOrder);
            Property(p => p.Type)
                .HasColumnOrder(++columnOrder);
        }
    }
}