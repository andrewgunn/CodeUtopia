namespace CodeUtopia.Domain
{
    public interface IVersionNumberProvider
    {
        int GetNextVersionNumber();

        void UpdateVersionNumber(int versionNumber);
    }
}