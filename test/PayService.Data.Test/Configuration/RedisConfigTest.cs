using PayService.Data.Configuration;
using Xunit;

namespace PayService.Data.Test.Configuration
{
    public class RedisConfigTest
    {
        private RedisConfig _config;

        public RedisConfigTest()
        {
            _config = ConfigurationManager.GetInstance().RedisConfig;
        }


        [Fact]
        public void ConnectionStringIsSetTest()
        {
            Assert.NotNull(_config.ConnectionString);
            Assert.NotEmpty(_config.ConnectionString);
        }
    }
}
