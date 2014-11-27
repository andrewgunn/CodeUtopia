using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BankingManagementClient.ProjectionStore.EntityFramework.AccountDetail;
using BankingManagementClient.ProjectionStore.EntityFramework.Client;
using BankingManagementClient.ProjectionStore.EntityFramework.ClientDetail;

namespace BankingManagementClient.ProjectionStore.EntityFramework
{
    public class ProjectionStoreContext : DbContext
    {
        public ProjectionStoreContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Aggregate
            modelBuilder.Configurations.Add(new AggregateEntityConfiguration());

            // AccountDetail
            modelBuilder.Configurations.Add(new AccountDetailEntityConfiguration());

            // Client
            modelBuilder.Configurations.Add(new ClientEntityConfiguration());

            // ClientDetail
            modelBuilder.Configurations.Add(new AccountEntityConfiguration());
            modelBuilder.Configurations.Add(new BankCardEntityConfiguration());
            modelBuilder.Configurations.Add(new ClientDetailEntityConfiguration());
        }

        public IDbSet<AccountDetailEntity> AccountDetails { get; set; }

        public IDbSet<AccountEntity> Accounts { get; set; }

        public IDbSet<AggregateEntity> Aggregates { get; set; }

        public IDbSet<BankCardEntity> BankCards { get; set; }

        public IDbSet<ClientDetailEntity> ClientDetails { get; set; }

        public IDbSet<ClientEntity> Clients { get; set; }
    }
}