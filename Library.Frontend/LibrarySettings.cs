using CodeUtopia;

namespace Library.Frontend
{
    public class LibrarySettings : ILibrarySettings
    {
        public LibrarySettings(ISettingsProvider settingsProvider)
        {
            _endpointName = settingsProvider.ApplicationSetting("Library.Frontend:EndpointName");
        }

        public string EndpointName
        {
            get
            {
                return _endpointName;
            }
        }

        private readonly string _endpointName;
    }
}