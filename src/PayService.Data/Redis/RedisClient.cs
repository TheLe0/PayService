using PayService.Data.Configuration;
using StackExchange.Redis;
using System.Net;

namespace PayService.Data.Redis
{
    public class RedisClient
    {
        private readonly string? _connectionString;
        private readonly ConnectionMultiplexer? _redis;
        public IDatabase Database { get;  }
        public IServer Server { get; }

        public RedisClient(DatabaseType type)
        {
            _connectionString = ConfigurationManager.GetInstance().RedisConfig.ConnectionString;
            _redis = ConnectionMultiplexer.Connect(_connectionString);
            EndPoint endPoint = _redis.GetEndPoints().First();
            Database = _redis.GetDatabase((int) type);
            Server = _redis.GetServer(endPoint);
        }
    }
}
