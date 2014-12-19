using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Library.Frontend.ProjectionStore.Aggregate;
using Library.Frontend.ProjectionStore.Book;

namespace Library.Frontend.ProjectionStore
{
    public class ProjectionStoreContext : DbContext
    {
        static ProjectionStoreContext()
        {
            Database.SetInitializer(new NullDatabaseInitializer<ProjectionStoreContext>());
        }

        public ProjectionStoreContext(IProjectionStoreDatabaseSettings projectionStoreDatabaseSettings)
            : base(projectionStoreDatabaseSettings.ConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
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