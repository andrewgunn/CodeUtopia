using System.Data.Entity.ModelConfiguration;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.AccountDetail
{
    public class AccountDetailEntityConfiguration : EntityTypeConfiguration<AccountDetailEntity>
    {
        public AccountDetailEntityConfiguration()
        {
            ToTable("AccountDetail", "AccountDetail");

            var columnOrder = 0;

            Property(p => p.Id)
                .HasColumnName("AccountId")
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.AccountName)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.Balance)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
        }
    }
}