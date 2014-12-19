using System.Configuration;

namespace CodeUtopia.Configuration
{
    public class ConfigurationManagerSettingsProvider : ISettingsProvider
    {
        public string ApplicationSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public string ConnectionString(string key)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[key];

            return connectionString != null ? connectionString.ConnectionString : null;
        }
    }
}