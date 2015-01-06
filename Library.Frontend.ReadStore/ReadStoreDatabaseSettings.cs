using CodeUtopia;

namespace Library.Frontend.ReadStore
{
    public class ReadStoreDatabaseSettings : IReadStoreDatabaseSettings
    {
        public ReadStoreDatabaseSettings(string readStoreConnectionStringKey, ISettingsProvider settingsProvider)
        {
            _readStoreConnectionStringKey = readStoreConnectionStringKey;
            _settingsProvider = settingsProvider;
        }

        public string ConnectionString
        {
            get
            {
                return _settingsProvider.ConnectionString(_readStoreConnectionStringKey);
            }
        }

        private readonly string _readStoreConnectionStringKey;

        private readonly ISettingsProvider _settingsProvider;
    }
}