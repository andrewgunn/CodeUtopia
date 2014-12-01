using System.Data.Entity.ModelConfiguration;

namespace BankReporting.ProjectionStore.EntityFramework.ClientDetail
{
    public class BankCardEntityConfiguration : EntityTypeConfiguration<BankCardEntity>
    {
        public BankCardEntityConfiguration()
        {
            ToTable("BankCard", "ClientDetail");

            HasKey(x => x.BankCardId);

            var columnOrder = 0;

            Property(p => p.BankCardId)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.ClientId)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.AccountId)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.IsStolen)
                .HasColumnOrder(++columnOrder);
            Property(p => p.StolenAt)
                .HasColumnOrder(++columnOrder);
        }
    }
}