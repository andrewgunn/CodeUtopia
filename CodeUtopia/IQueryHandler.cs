namespace CodeUtopia
{
    public interface IQueryHandler<in TQuery, out TProjection>
        where TQuery : IQuery<TProjection>
    {
        TProjection Handle(TQuery query);
    }
}