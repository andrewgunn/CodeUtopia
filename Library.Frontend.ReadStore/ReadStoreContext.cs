using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Library.Frontend.ReadStore.Aggregate;
using Library.Frontend.ReadStore.Book;

namespace Library.Frontend.ReadStore
{
    public class ReadStoreContext : DbContext
    {
        static ReadStoreContext()
        {
            Database.SetInitializer(new NullDatabaseInitializer<ReadStoreContext>());
        }

        public ReadStoreContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public ReadStoreContext(IReadStoreDatabaseSettings readStoreDatabaseSettings)
            : this(readStoreDatabaseSettings.ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Aggregate
            modelBuilder.Configurations.Add(new AggregateEntityConfiguration());

            // Book
            modelBuilder.Configurations.Add(new BookEntityConfiguration());
        }

        public IDbSet<AggregateEntity> Aggregates { get; set; }

        public IDbSet<BookEntity> Books { get; set; }
    }
}