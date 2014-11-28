using System.Data.Entity.ModelConfiguration;

namespace BankingManagementClient.ProjectionStore.EntityFramework.AccountDetail
{
    public class AccountDetailEntityConfiguration : EntityTypeConfiguration<AccountDetailEntity>
    {
        public AccountDetailEntityConfiguration()
        {
            ToTable("AccountDetail", "AccountDetail");

            HasKey(x => x.AccountId);

            var columnOrder = 0;

            Property(p => p.AccountId)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.ClientId)
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