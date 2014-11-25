using System.Data.Entity.ModelConfiguration;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.ClientDetail
{
    public class BankCardEntityConfiguration : EntityTypeConfiguration<BankCardEntity>
    {
        public BankCardEntityConfiguration()
        {
            ToTable("BankCard", "ClientDetail");

            var columnOrder = 0;

            Property(p => p.Id)
                .HasColumnName("ClientId")
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.AccountId)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
        }
    }
}