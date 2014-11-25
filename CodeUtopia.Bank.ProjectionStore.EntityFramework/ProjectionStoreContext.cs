using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CodeUtopia.Bank.ProjectionStore.EntityFramework.AccountDetail;
using CodeUtopia.Bank.ProjectionStore.EntityFramework.Client;
using CodeUtopia.Bank.ProjectionStore.EntityFramework.ClientDetail;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework
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

        public IDbSet<ClientDetailEntity> ClientDetails { get; set; }

        public IDbSet<ClientEntity> Clients { get; set; }
    }
}