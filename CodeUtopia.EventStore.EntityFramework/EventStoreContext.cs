using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CodeUtopia.EventStore.EntityFramework
{
    public class EventStoreContext : DbContext
    {
        public EventStoreContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new DomainEventEntityConfiguration());
            modelBuilder.Configurations.Add(new SnapshotEntityConfiguration());
        }

        public IDbSet<DomainEventEntity> DomainEvents { get; set; }

        public IDbSet<SnapshotEntity> Snapshots { get; set; }
    }
}