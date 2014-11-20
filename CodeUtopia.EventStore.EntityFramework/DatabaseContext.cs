using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CodeUtopia.EventStore.EntityFramework
{
    internal class DatabaseContext : DbContext
    {
        public DatabaseContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new DomainEventConfiguration());
            modelBuilder.Configurations.Add(new AggregateConfiguration());
            modelBuilder.Configurations.Add(new SnapshotConfiguration());
        }

        public IDbSet<Aggregate> Aggregates { get; set; }

        public IDbSet<DomainEvent> DomainEvents { get; set; }

        public IDbSet<Snapshot> Snapshots { get; set; }
    }
}