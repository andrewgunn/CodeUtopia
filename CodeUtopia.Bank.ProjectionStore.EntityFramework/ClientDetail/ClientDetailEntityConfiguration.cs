using System.Data.Entity.ModelConfiguration;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.ClientDetail
{
    public class ClientDetailEntityConfiguration : EntityTypeConfiguration<ClientDetailEntity>
    {
        public ClientDetailEntityConfiguration()
        {
            ToTable("ClientDetail", "ClientDetail");

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