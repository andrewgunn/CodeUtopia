namespace Library.Frontend.Queries.Projections.ReadStoreStatus
{
    public class ReadStoreStatusProjection
    {
        public ReadStoreStatusProjection(bool isEmpty)
        {
            _isEmpty = isEmpty;
        }

        public bool IsEmpty
        {
            get
            {
                return _isEmpty;
            }
        }

        private readonly bool _isEmpty;
    }
}