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
            _connectionString = GetConnectionString();
            _redis = ConnectionMultiplexer.Connect(_connectionString);
            Database = _redis.GetDatabase((int) type);
        }

        private static string GetConnectionString()
        {
            #if DEBUG
                return "payservice.redis.cache.windows.net,abortConnect=false,ssl=true,allowAdmin=true,password=IfT8aZKIMHOhfvVO4uAuFaHEm7LlmI4IWAzCaG8qkms=";
            #else
                return string.Empty;
            #endif
        }
    }
}
