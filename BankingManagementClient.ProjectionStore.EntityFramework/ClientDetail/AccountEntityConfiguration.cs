using System.Data.Entity.ModelConfiguration;

namespace BankingManagementClient.ProjectionStore.EntityFramework.ClientDetail
{
    public class AccountEntityConfiguration : EntityTypeConfiguration<AccountEntity>
    {
        public AccountEntityConfiguration()
        {
            ToTable("Account", "ClientDetail");

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
        }
    }
}