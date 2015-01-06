using System.Linq;
using CodeUtopia;
using Library.Frontend.Queries;
using Library.Frontend.Queries.Projections.ReadStoreStatus;

namespace Library.Frontend.ReadStore.QueryHandlers
{
    public class ReadStoreStatusQueryHandler : IQueryHandler<ReadStoreStatusQuery, ReadStoreStatusProjection>
    {
        public ReadStoreStatusQueryHandler(IReadStoreDatabaseSettings readStoreDatabaseSettings)
        {
            _readStoreDatabaseSettings = readStoreDatabaseSettings;
        }

        public ReadStoreStatusProjection Handle(ReadStoreStatusQuery query)
        {
            using (var databaseContext = new ReadStoreContext(_readStoreDatabaseSettings))
            {
                var isEmpty = databaseContext.Aggregates.Any();

                return new ReadStoreStatusProjection(isEmpty);
            }
        }

        private readonly IReadStoreDatabaseSettings _readStoreDatabaseSettings;
    }
}