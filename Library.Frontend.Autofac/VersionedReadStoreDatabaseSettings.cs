using CodeUtopia;
using Library.Frontend.ReadStore;

namespace Library.Frontend.Autofac
{
    public class VersionedReadStoreDatabaseSettings : IReadStoreDatabaseSettings
    {
        public VersionedReadStoreDatabaseSettings(ILibrarySettings librarySettings, ISettingsProvider settingsProvider)
        {
            _connectionString = settingsProvider.ConnectionString(string.Format("ReadStore_v{0}", librarySettings.VersionNumber));
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