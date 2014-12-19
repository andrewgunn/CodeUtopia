namespace CodeUtopia
{
    public sealed class QueryDispatcher : IQueryDispatcher
    {
        public QueryDispatcher(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            var queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic queryHandler = _dependencyResolver.Resolve(queryHandlerType);

            return queryHandler.Handle((dynamic)query);
        }

        private readonly IDependencyResolver _dependencyResolver;
    }
}