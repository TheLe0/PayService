using System.Text.Json;
using System.Text.Json.Serialization;
using StackExchange.Redis;

namespace PayService.Customer.Data
{
    public class CustomerRepository
    {
        private readonly string _connectionString = "payservice.redis.cache.windows.net,abortConnect=false,ssl=true,allowAdmin=true,password=IfT8aZKIMHOhfvVO4uAuFaHEm7LlmI4IWAzCaG8qkms=";
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public CustomerRepository()
        {
            _redis = ConnectionMultiplexer.Connect(_connectionString);
            _database = _redis.GetDatabase(1);
        }

        public async Task<Customer?> FindByCpf(string cpf)
        {
            try
            {
                var result = await _database.StringGetAsync(cpf);

                if (result == string.Empty)
                {
                    return null;
                }

                var customer = JsonSerializer.Deserialize<Customer>(result);

                if (customer == null)
                {
                    return null;
                }

                return new Customer(customer.Name, customer.State, cpf);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<Customer?> InsertNewCustomer(Customer customer)
        {
            try
            {
                var result = await FindByCpf(customer.Cpf);
                if (result != null)
                {
                    return result;
                }

                await _database.StringSetAsync(customer.Cpf, JsonSerializer.Serialize(customer));

                return customer;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
