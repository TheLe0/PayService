using Microsoft.Extensions.Configuration;

namespace PayService.Data.Configuration
{
    public sealed class ConfigurationManager
    {
        private static ConfigurationManager? _instance;
        public RedisConfig  RedisConfig { get; private set; }
        private ConfigurationManager() 
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(GetSettingsFileName()).Build();

            var section = config.GetSection(nameof(RedisConfig));
            RedisConfig = section.Get<RedisConfig>();
        }

        public static ConfigurationManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ConfigurationManager();
            }
            return _instance;
        }

        private static string GetSettingsFileName()
        {
            #if DEBUG
                return "settings_dev.json";
            #else
                return "settings_prod.json";
            #endif
        }
    }
}
