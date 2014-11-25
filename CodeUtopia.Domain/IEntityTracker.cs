namespace CodeUtopia.Domain
{
    public interface IEntityTracker
    {
        void RegisterForTracking(IEntity entity);
    }
}