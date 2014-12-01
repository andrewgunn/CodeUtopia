using System.Data.Entity.ModelConfiguration;

namespace BankReporting.ProjectionStore.Client
{
    public class ClientEntityConfiguration : EntityTypeConfiguration<ClientEntity>
    {
        public ClientEntityConfiguration()
        {
            ToTable("Client", "Client");

            HasKey(x => x.ClientId);

            var columnOrder = 0;

            Property(p => p.ClientId)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.ClientName)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
        }
    }
}