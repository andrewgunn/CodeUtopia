using CodeUtopia;

namespace Library.Frontend
{
    public class LibrarySettings : ILibrarySettings
    {
        public LibrarySettings(ISettingsProvider settingsProvider)
        {
            _versionNumber = int.Parse(settingsProvider.ApplicationSetting("Library.Frontend:VersionNumber"));
        }

        public int VersionNumber
        {
            get
            {
                return _versionNumber;
            }
        }

        private readonly int _versionNumber;
    }
}