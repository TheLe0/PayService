using PayService.Data.Redis;
using Xunit;

namespace PayService.Data.Test.Redis
{
    public class RedisClientTest
    {
        private RedisClient? _redis;

        [Fact]
        public async Task ConnectCustomersDBTest()
        {
            _redis = new RedisClient(DatabaseType.CUSTOMERS);
            var pong = await _redis.Database.PingAsync();

            Assert.True(pong > TimeSpan.Zero);
        }

        [Fact]
        public async Task ConnectTransactionsByCpfDBTest()
        {
            _redis = new RedisClient(DatabaseType.TRANSACTIONS_BY_CPF);
            var pong = await _redis.Database.PingAsync();

            Assert.True(pong > TimeSpan.Zero);
        }

        [Fact]
        public async Task ConnectTransactionsByMonthDBTest()
        {
            _redis = new RedisClient(DatabaseType.TRANSACTIONS_BY_MONTH);
            var pong = await _redis.Database.PingAsync();

            Assert.True(pong > TimeSpan.Zero);
        }
    }
}
