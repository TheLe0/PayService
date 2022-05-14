using PayService.Data.Configuration;
using StackExchange.Redis;

namespace PayService.Data.Redis
{
    public class RedisClient
    {
        private readonly string _connectionString;
        private readonly ConnectionMultiplexer _redis;
        public IDatabase Database { get;  }

        public RedisClient(DatabaseType type)
        {
            _connectionString = ConfigurationManager.GetInstance().RedisConfig.ConnectionString;
            _redis = ConnectionMultiplexer.Connect(_connectionString);
            Database = _redis.GetDatabase((int) type);
        }
    }
}
