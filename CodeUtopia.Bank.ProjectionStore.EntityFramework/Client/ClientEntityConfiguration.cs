using System.Data.Entity.ModelConfiguration;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.Client
{
    public class ClientEntityConfiguration : EntityTypeConfiguration<ClientEntity>
    {
        public ClientEntityConfiguration()
        {
            ToTable("Client", "Client");

            var columnOrder = 0;

            Property(p => p.Id)
                .HasColumnName("ClientId")
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.ClientName)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
        }
    }
}