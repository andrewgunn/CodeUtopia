namespace CodeUtopia
{
    public interface IQueryHandler<in TQuery>
        where TQuery : class
    {
        void Handle(TQuery query);
    }
}