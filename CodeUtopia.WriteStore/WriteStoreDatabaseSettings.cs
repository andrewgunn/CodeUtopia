namespace CodeUtopia.WriteStore
{
    public class WriteStoreDatabaseSettings : IWriteStoreDatabaseSettings
    {
        public WriteStoreDatabaseSettings(ISettingsProvider settingsProvider)
        {
            _connectionString = settingsProvider.ConnectionString("WriteStore");
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