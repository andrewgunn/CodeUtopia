using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeUtopia.WriteStore.EntityFramework
{
    public class WriteStoreContext : DbContext
    {
        static WriteStoreContext()
        {
            Database.SetInitializer(new NullDatabaseInitializer<WriteStoreContext>());
        }

        public WriteStoreContext(string nameOrConnectionString)
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

        public override int SaveChanges()
        {
            var exceptions = ChangeTracker.Entries<DomainEventEntity>()
                                          .Where(x => x.State == EntityState.Deleted)
                                          .Select(x => x.Entity)
                                          .ToList()
                                          .Select(
                                                  deletedDomainEvent =>
                                                  new DomainEventsCannotBeDeletedException(deletedDomainEvent))
                                          .Cast<Exception>()
                                          .Concat(ChangeTracker.Entries<DomainEventEntity>()
                                                               .Where(x => x.State == EntityState.Modified)
                                                               .Select(x => x.Entity)
                                                               .ToList()
                                                               .Select(
                                                                       updatedDomainEvent =>
                                                                       new DomainEventsCannotBeUpdatedException(
                                                                           updatedDomainEvent)))
                                          .ToList();

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }

            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException dbUpdateException)
            {
                var sqlException = dbUpdateException.GetBaseException() as SqlException;

                if (sqlException == null)
                {
                    throw;
                }

                var match = Regex.Match(sqlException.Message,
                                        @"Violation of PRIMARY KEY constraint '[^']+'. Cannot insert duplicate key in object '[^']+'. The duplicate key value is \(([^\)]+)\)");

                if (!match.Success)
                {
                    throw;
                }

                throw new PrimaryKeyConstraintViolationException(Convert.ToString(match.Groups[1])
                                                                        .Split(',')
                                                                        .Select(x => x.Trim())
                                                                        .ToArray());
            }
        }

        public IDbSet<DomainEventEntity> DomainEvents { get; set; }

        public IDbSet<SnapshotEntity> Snapshots { get; set; }
    }
}