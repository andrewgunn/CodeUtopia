using CodeUtopia;

namespace Library.Frontend.ProjectionStore
{
    public class ProjectionStoreDatabaseSettings : IProjectionStoreDatabaseSettings
    {
        public ProjectionStoreDatabaseSettings(string projectionStoreConnectionStringKey,
                                               ISettingsProvider settingsProvider)
        {
            _projectionStoreConnectionStringKey = projectionStoreConnectionStringKey;
            _settingsProvider = settingsProvider;
            ;
        }

        public string ConnectionString
        {
            get
            {
                return _settingsProvider.ConnectionString(_projectionStoreConnectionStringKey);
            }
        }

        private readonly string _projectionStoreConnectionStringKey;

        private readonly ISettingsProvider _settingsProvider;
    }
}