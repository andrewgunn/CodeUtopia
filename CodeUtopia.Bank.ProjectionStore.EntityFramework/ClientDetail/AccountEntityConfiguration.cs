using System.Data.Entity.ModelConfiguration;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.ClientDetail
{
    public class AccountEntityConfiguration : EntityTypeConfiguration<AccountEntity>
    {
        public AccountEntityConfiguration()
        {
            ToTable("Account", "ClientDetail");

            var columnOrder = 0;

            Property(p => p.Id)
                .HasColumnName("AccountId")
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.ClientId)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.AccountName)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
        }
    }
}