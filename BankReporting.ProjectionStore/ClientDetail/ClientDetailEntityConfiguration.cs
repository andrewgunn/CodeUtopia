using System.Data.Entity.ModelConfiguration;

namespace BankReporting.ProjectionStore.ClientDetail
{
    public class ClientDetailEntityConfiguration : EntityTypeConfiguration<ClientDetailEntity>
    {
        public ClientDetailEntityConfiguration()
        {
            ToTable("ClientDetail", "ClientDetail");

            HasKey(x => x.ClientId);

            var columnOrder = 0;

            Property(p => p.ClientId)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.ClientName)
                .HasColumnOrder(++columnOrder)
                .IsRequired();

            HasMany(x => x.Accounts)
                .WithOptional()
                .HasForeignKey(x => x.ClientId);
            HasMany(x => x.BankCards)
                .WithOptional()
                .HasForeignKey(x => x.ClientId);
        }
    }
}