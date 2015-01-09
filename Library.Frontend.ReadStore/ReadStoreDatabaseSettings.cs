using CodeUtopia;

namespace Library.Frontend.ReadStore
{
    public class ReadStoreDatabaseSettings : IReadStoreDatabaseSettings
    {
        public ReadStoreDatabaseSettings(ISettingsProvider settingsProvider)
        {
            _connectionString = settingsProvider.ConnectionString("ReadStore");
        }

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        private readonly string _connectionString;
    }
}