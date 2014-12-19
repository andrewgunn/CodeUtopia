namespace CodeUtopia
{
    public interface ISettingsProvider
    {
        string ApplicationSetting(string key);

        string ConnectionString(string key);
    }
}